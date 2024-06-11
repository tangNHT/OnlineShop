using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    public class AdminController : BaseController
	{
        public IActionResult Index()
        {
            return View();
        }
    }
}
