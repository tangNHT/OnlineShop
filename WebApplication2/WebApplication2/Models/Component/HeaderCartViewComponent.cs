using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Composition.Convention;
using WebApplication2.Common;

namespace WebApplication2.Models.Component
{
	public class HeaderCartViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			var listCartJson = HttpContext.Session.GetString(CommonConstants.CartSession);
			var listCart = new List<CartItem>();
			if (!string.IsNullOrEmpty(listCartJson))
			{
				listCart = JsonConvert.DeserializeObject<List<CartItem>>(listCartJson);
			}

			return View(listCart);
		}
	}
}
