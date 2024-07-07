using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Models.Component
{
	public class TopMenuViewComponent : ViewComponent
	{
        [ResponseCache(Duration = 3600 * 24, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IViewComponentResult Invoke()
		{
			var model = new MenuModel().ListByGroupId(2);
			return View(model); 
		}
	}
}
