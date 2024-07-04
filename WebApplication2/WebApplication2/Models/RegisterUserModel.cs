using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
	public class RegisterUserModel
	{
		[Key]
		public int Id { get; set; }
		[Display(Name = "Tên đăng nhập")]
		[Required(ErrorMessage = "Bạn hãy nhập tên đăng nhập")]
		public string? UserName { get; set; }
		[Display(Name = "Mật khẩu")]
		[Required(ErrorMessage = "Bạn hãy nhập mật khẩu đăng nhập")]
		[StringLength(20,MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }
		[Display(Name = "Xác nhận mật khẩu")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Xác nhận mật khẩu không đúng")]
		public string? ConfirmPassword { get; set; }
		[Display(Name = "Họ Tên")]
		[Required(ErrorMessage = "Bạn hãy nhập Họ Tên")]
		public string? Name { get; set; }
		[Display(Name = "Email")]
		[Required(ErrorMessage = "Bạn hãy nhập Email")]
		public string? Email { get; set; }
		[Display(Name = "Địa chỉ")]
		public string? Address { get; set; }
		[Display(Name = "Điện thoại")]
		public string? Phone { get; set; }
	}
}
