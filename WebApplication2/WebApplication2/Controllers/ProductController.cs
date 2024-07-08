using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult ProductCategory (int cateId, int page = 1, int pageSize = 4)
		{
			var category = new ProductCategoryModel().ViewDetail(cateId);
			ViewBag.Category = category;
			int totalRecord = 0;
			var model = new ProductModel().ListByCategoryId(cateId, ref totalRecord, page, pageSize);
			ViewBag.Total = totalRecord;
			ViewBag.Page = page;

			int maxPage = 5;
			int totalPage = 0;
			totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
			ViewBag.TotalPage = totalPage;
			ViewBag.MaxPage = maxPage;
			ViewBag.First = 1;
			ViewBag.Last = totalPage;
			ViewBag.Next = page + 1;
			ViewBag.Prev = page - 1;

            return View(model);
		}

		public IActionResult Detail(int id)
		{
			var product = new ProductModel().ViewDetail(id);
			ViewBag.ProductCategory = new ProductCategoryModel().ViewDetail(product.CategoryId.Value);
			ViewBag.RelateProduct = new ProductModel().ListRelateProduct(id);
			return View(product);
		}
		
		public JsonResult ListName(string q)
		{
			var data = new ProductModel().ListName(q);
			return Json(new
			{
				data = data,
				status = true
			});
		}

        public IActionResult Search(string keyword, int page = 1, int pageSize = 4)
        {
            int totalRecord = 0;
            var model = new ProductModel().Search(keyword, ref totalRecord, page, pageSize);
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

			ViewBag.Keyword = keyword;

            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
    }
}
