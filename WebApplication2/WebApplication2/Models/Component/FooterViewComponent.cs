using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Models.Component
{
	public class FooterViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var model = new FooterModel().GetFooter();
			return View(model); 
		}
	}
}
