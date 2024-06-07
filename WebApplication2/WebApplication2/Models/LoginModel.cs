using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Bạn hãy nhập tài khoản đăng nhập")]
		public string UserName { get; set; }
        [Required(ErrorMessage = "Bạn hãy nhập mật khẩu đăng nhập")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
