using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication2.Models
{
	public class RegisterModel
	{
		public int Id { get; set; }
		[DisplayName("Tài khoản")]
		[Required(ErrorMessage = "Bạn hãy nhập tài khoản đăng nhập")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Bạn hãy nhập mật khẩu đăng nhập")]
		[DisplayName("Mật khẩu")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required(ErrorMessage = "Bạn hãy nhập lại mật khẩu")]
		[DisplayName("Nhập lại mật khẩu")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Mật khẩu chưa trùng khớp")]
		public string RepeatPassword { get; set; }
		public string? Admin { get; set; }
		[DisplayName("Họ")]
		public string FirstName { get; set; }
		[DisplayName("Tên")]
		public string LastName { get; set; }
	}
}
