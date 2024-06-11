using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class CategoryController : BaseController
	{
		// GET: CategoryController
		public ActionResult Index(int pageNumber = 1, int pageSize = 10)
		{
			//Tạo Model
			var iplCategory = new CategoryModel();
			var categories = iplCategory.ListAll(pageNumber, pageSize);
			var totalPages = iplCategory.TotalPages(pageSize);
			var totalItems = iplCategory.TotalItems();

			var model = new PaginationModel<Category>
			{
				Items = categories,
				PageNumber = pageNumber,
				TotalPages = totalPages,
				TotalItems = totalItems
			};
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
					{
						TempData["SuccessMessage"] = "Thêm mới danh mục thành công";
						//Chuyển đến Action khác
						return RedirectToAction(nameof(Index));
					}
					else
					{
						TempData["ErrorMessage"] = "Danh mục đã tồn tại";
						ModelState.AddModelError("Create Category False", "Thêm mới không thành công");
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
			var user = new CategoryModel().ViewDetail(id);
			return View(user);
		}

		// POST: CategoryController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Category collection)
		{
			try
			{
				//Xử lý dữ liệu hợp lệ
				if (ModelState.IsValid)
				{
					var model = new CategoryModel();
					var res = model.Update(collection);
					if (res)
					{
						TempData["SuccessMessage"] = "Cập nhật danh mục thành công";
						//Chuyển đến Action khác
						return RedirectToAction(nameof(Index));
					}
					else
					{
						TempData["ErrorMessage"] = "Cập nhật không thành công";
						ModelState.AddModelError("Create Category False", "Cập nhật không thành công");
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
