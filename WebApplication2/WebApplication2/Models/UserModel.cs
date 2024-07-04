using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

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
