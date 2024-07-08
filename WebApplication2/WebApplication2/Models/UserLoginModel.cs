using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class UserLoginModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập của bạn")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu đăng nhập")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}
