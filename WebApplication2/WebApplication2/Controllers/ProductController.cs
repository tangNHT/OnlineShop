using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ProductCategory (int cateId)
		{
            return View();
		}

		public IActionResult Detail(int id)
		{
			var product = new ProductModel().ViewDetail(id);
			ViewBag.ProductCategory = new ProductCategoryModel().ViewDetail(product.CategoryId.Value);
			ViewBag.RelateProduct = new ProductModel().ListRelateProduct(id);
			return View();
		}
	}
}
