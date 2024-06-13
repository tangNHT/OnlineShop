using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Models.Component
{
	public class ProductCategoryViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var model = new ProductCategoryModel().ListAll();
			return View(model); 
		}
	}
}
