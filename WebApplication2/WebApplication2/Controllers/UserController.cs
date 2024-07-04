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
