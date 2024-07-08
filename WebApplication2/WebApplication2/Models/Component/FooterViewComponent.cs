using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Models.Component
{
	public class FooterViewComponent : ViewComponent
	{
        [ResponseCache(Duration = 3600*24, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IViewComponentResult Invoke()
		{
			var model = new FooterModel().GetFooter();
			return View(model); 
		}
	}
}
