using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApplication2.Common;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.Slides = new SildeModel().ListAll();
			var productModel = new ProductModel();
			var sessionValue = HttpContext.Session.GetString(CommonConstants.USER_SESSION);
			var userName = new UserModel();
			ViewBag.UserName = userName.GetName(sessionValue);
			ViewBag.NewProducts = productModel.ListNewProduct(4);
			ViewBag.ListFeatureProducts = productModel.ListFeatureProduct(4);
			return View();
		}
	}
}
