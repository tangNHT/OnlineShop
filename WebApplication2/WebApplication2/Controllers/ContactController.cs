using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            var model = new ContactModel().GetActiveContact();
            return View(model);
        }
    }
}
