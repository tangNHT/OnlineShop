using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Models.Component
{
	public class TopMenuViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var model = new MenuModel().ListByGroupId(2);
			return View(model); 
		}
	}
}
