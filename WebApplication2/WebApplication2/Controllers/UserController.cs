using Microsoft.AspNetCore.Mvc;
using WebApplication2.Common;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Login(UserLoginModel model)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    var result = new UserModel().Login(model.UserName, model.Password);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Đăng nhập thành công";
                        HttpContext.Session.SetString(CommonConstants.USER_SESSION, model.UserName);
                        TempData["RedirectUrl"] = Url.Action("Index", "Home");
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
			//Xoá toàn bộ session
			HttpContext.Session.Clear();

			//Chuyển hướng về trang đăng nhập
			return Redirect("/");
		}

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Register(RegisterUserModel model)
		{
			if(ModelState.IsValid)
			{
				var userModel = new UserModel();
				if (userModel.CheckUserName(model.UserName)){
                    TempData["ErrorMessage"] = "Tên đăng nhập đã tồn tại";
                }
				else if (userModel.CheckEmail(model.Email))
				{
                    TempData["ErrorMessage"] = "Email đã tồn tại";
                }
				else
				{
					var result = userModel.Register(model.UserName, model.Password, model.Name, model.Email, model.Phone, model.Address);
					if (result)
					{
                        TempData["SuccessMessage"] = "Đăng ký thành công";
                        HttpContext.Session.SetString(CommonConstants.CartSession, string.Empty);
                        TempData["RedirectUrl"] = Url.Action("Index", "Home");
                    }
					else
					{
                        TempData["ErrorMessage"] = "Đăng ký không thành công";
                    }
				}
			}

			return View(model);
		}
	}
}
