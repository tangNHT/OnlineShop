using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using WebApplication2.Common;

namespace WebApplication2.Models
{
    public class UserModel
    {
        private OnlineShopDbContext context;
        public UserModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
            var options = optionsBuilder.Options;
            context = new OnlineShopDbContext(options);
        }
        public bool CheckUserName (string userName)
        {
            return context.Users.Count(x => x.UserName == userName) > 0;
        }
        public bool CheckEmail (string email)
        {
            return context.Users.Count(x => x.Email == email) > 0;
        }
        public int Insert(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user.Id;
        }

        public bool Register(string userName, string password, string name, string email, string phone, string address)
        {
            try
            {
                //Kiểm tra tài khoản đã tồn tại hay chưa
                var account = context.Accounts.FirstOrDefault(a => a.UserName == userName);
                if (account == null)
                {
                    //Tạo tài khoản mới
                    var newUser = new User()
                    {
                        UserName = userName,
                        Password = HashPassword(password),
                        Name = name,
                        Email = email,
                        Phone = phone,
                        Address = address,
                        CreatedDate = DateTime.Now,
                        Status = true
                    };
                    //Thêm mới tài khoản vào db
                    context.Users.Add(newUser);
                    //Lưu thay đổi
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public bool Login(string userName, string password, bool isLoginAdmin)
        {
            try
            {
                //Mã hoá mật khẩu nếu cần thiết (Ví dụ: băm mật khẩu)
                string hashedPassword = HashPassword(password);

                //Truy vấn cơ sở dữ liệu để tìm tài khoản có tên người dùng và mật khẩu đã mã hoá
                var account = context.Users.FirstOrDefault(a => a.UserName == userName && a.Password == hashedPassword);

				if (isLoginAdmin && (account.GroupId == CommonConstants.ADMIN_GROUP || account.GroupId == CommonConstants.MOD_GROUP))
				{
					return true;
				}

				// Trả về true nếu tài khoản tồn tại, ngược lại trả về false
				return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return false;
        }

        public string GetName(string userName)
        {
            return (from c in context.Users where c.UserName == userName select c.Name).FirstOrDefault();
        }

        private string HashPassword(string password)
        {
            //Sử dụng phương thức băm mật khẩu phù hợp với ứng dụng của bạn
            //Ví dụ dưới đây sử dụng SHA256 để băm mật khẩu
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
