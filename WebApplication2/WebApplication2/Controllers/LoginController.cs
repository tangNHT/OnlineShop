using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Common;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//
        public IActionResult Index(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new AccountModel().Login(model.UserName, model.Password);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Đăng nhập thành công";
                        HttpContext.Session.SetString(CommonConstants.ADMIN_SESSION, model.UserName);
                        TempData["RedirectUrl"] = Url.Action("Index", "Admin");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Tài khoản hoặc mật khẩu không chính xác";
                        ModelState.AddModelError("LoginError", "Tài khoản hoặc mật khẩu không chính xác");
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
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
