using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
	public class RegisterAccountController : Controller
	{
		// GET: RegisterController
		public ActionResult Index()
		{
			return View();
		}

		// GET: RegisterController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: RegisterController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: RegisterController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(RegisterModel collection)
		{
			try
			{
				if(ModelState.IsValid)
				{
					var model = new AccountModel();
					var result = model.Register(collection.UserName, collection.Password, collection.FirstName, collection.LastName);
					if (result)
					{
						//Lưu thông báo thành công vào TempData
						TempData["SuccessMessage"] = "Thêm mới tài khoản thành công";
						//Lưu đường dẫn chuyển màn vào TempData
						TempData["RedirectUrl"] = Url.Action("Index", "Login");
					}
					else
					{
						//Lưu thông báo không thành công vào TempData
						TempData["ErrorMessage"] = "Tài khoản đã tồn tại";
						ModelState.AddModelError("Register False", "Tài khoản đã tồn tại");
					}
				}
				return View(collection);
			}
			catch
			{
				return View();
			}
		}

		// GET: RegisterController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: RegisterController/Edit/5
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

		// GET: RegisterController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: RegisterController/Delete/5
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
