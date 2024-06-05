using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication2.Models
{
    public class AccountModel
    {
		private OnlineShopDbContext context;
        public AccountModel() 
        {
			var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
			var options = optionsBuilder.Options;
			context = new OnlineShopDbContext(options);         
        }
        public bool Login(string userName, string password)
        {
			try
			{
				//Mã hoá mật khẩu nếu cần thiết (Ví dụ: băm mật khẩu)
				string hashedPassword = HashPassword(password);

				//Truy vấn cơ sở dữ liệu để tìm tài khoản có tên người dùng và mật khẩu đã mã hoá
				var account = context.Accounts.FirstOrDefault(a => a.UserName == userName && a.Password == hashedPassword);

				// Trả về true nếu tài khoản tồn tại, ngược lại trả về false
				return account != null;
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
