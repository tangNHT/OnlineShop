using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class CategoryController : Controller
	{
		// GET: CategoryController
		public ActionResult Index()
		{
			var iplCategory = new CategoryModel();
			var model = iplCategory.ListAll();
			return View(model);
		}

		// GET: CategoryController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CategoryController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CategoryController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Category collection)
		{
			try
			{
				//Xử lý dữ liệu hợp lệ
				if (ModelState.IsValid)
				{
					var model = new CategoryModel();
					int res = await model.Create(collection.Name, collection.Alias, collection.ParentId, collection.Order, collection.Status);
					if(res > 0)
					//Chuyển đến Action khác
					return RedirectToAction(nameof(Index));
					else
					{
						ModelState.AddModelError("", "Thêm mới không thành công");
					}
				}
				//Trả lại trang với dữ liệu đã nhập và thông báo lỗi
				return View(collection);
			}
			catch
			{
				return View();
			}
		}

		// GET: CategoryController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: CategoryController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: CategoryController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: CategoryController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
