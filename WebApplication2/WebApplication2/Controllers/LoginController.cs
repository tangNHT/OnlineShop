using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel model)
        {
            var result = new AccountModel().Login(model.UserName, model.Password);
            if(result && ModelState.IsValid)
            {
                HttpContext.Session.SetString("loginSession", model.UserName);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
            }
            return View(model);
        }
        public ActionResult Logout()
        {
			////Đăng xuất người dùng
			//HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			//         //Xoá cookie hoặc thông tin xác thực
			//         Response.Cookies.Delete(".AspNetCore.Cookies");

			//Xoá toàn bộ session
            HttpContext.Session.Clear();

			//Chuyển hướng về trang đăng nhập
			return RedirectToAction("Index", "Login");
		}
	}
}
