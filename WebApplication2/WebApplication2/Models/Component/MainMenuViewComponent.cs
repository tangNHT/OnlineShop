using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using WebApplication2.Models;
namespace WebApplication2.Models.Component
{
    public class MainMenuViewComponent : ViewComponent
    {
        [ResponseCache(Duration = 3600 * 24, Location = ResponseCacheLocation.Any, NoStore = false)]
        public IViewComponentResult Invoke()
        {
            //Hiển thị list danh sách trong bảng Menu
            var model = new MenuModel().ListByGroupId(1);
            return View(model);
        }
    }
}
