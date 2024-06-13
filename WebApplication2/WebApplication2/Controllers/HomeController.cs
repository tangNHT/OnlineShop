using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			ViewBag.Slides = new SildeModel().ListAll();
			var productModel = new ProductModel();
			ViewBag.NewProducts = productModel.ListNewProduct(4);
			ViewBag.ListFeatureProducts = productModel.ListFeatureProduct(4);
			return View();
		}
	}
}
