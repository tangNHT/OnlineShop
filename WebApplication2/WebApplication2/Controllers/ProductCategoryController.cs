using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
	public class ProductCategoryController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult Detail()
		{
			return View();
		}
	}
}
