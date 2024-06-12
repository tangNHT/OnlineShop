using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class CategoryController : BaseController
	{
		// GET: CategoryController
		public ActionResult Index(string searchString, int pageNumber = 1, int pageSize = 10)
		{
			//Tạo Model
			var iplCategory = new CategoryModel();

			var totalPages = iplCategory.TotalPages(pageSize);
			var totalItems = iplCategory.TotalItems();
			var categories = string.IsNullOrEmpty(searchString)
							? iplCategory.ListAll(pageNumber, pageSize)
							: iplCategory.ListAll(searchString, pageNumber, pageSize);

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
					if (res > 0)
					{
						TempData["SuccessMessage"] = "Thêm mới danh mục thành công";
						//Chuyển đến Action khác
						TempData["RedirectUrl"] = Url.Action("Index", "Category");
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
						TempData["RedirectUrl"] = Url.Action("Index", "Category");
					}
					else
					{
						TempData["ErrorMessage"] = "Cập nhật không thành công";
						ModelState.AddModelError("Edit Category False", "Cập nhật không thành công");
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

		// POST: CategoryController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id)
		{
			try
			{
				var model = new CategoryModel();
				var result = model.Delete(id);
				if (result)
				{
					TempData["SuccessMessage"] = "Xoá danh mục thành công";
					TempData["RedirectUrl"] = Url.Action("Index", "Category");
				}
				else
				{
					TempData["ErrorMessage"] = "Xoá danh mục thành công";
					ModelState.AddModelError("Delete Category False", "Xoá không thành công");
				}
				return RedirectToAction("Index");
			}
			catch
			{
				return View();
			}
		}

		[HttpPost]
		public JsonResult ChangeStatus (int id)
		{
			//Tạo model
			var collection = new CategoryModel();
			var result = collection.ChangeStatus(id);
			//Dữ liệu gửi đi
			return Json(new
			{
				status = result,
			});
		}
	}
}
