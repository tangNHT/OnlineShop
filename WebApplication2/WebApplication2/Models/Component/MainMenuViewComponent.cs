using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
namespace WebApplication2.Models.Component
{
    public class MainMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            //Hiển thị list danh sách trong bảng Menu
            var model = new MenuModel().ListByGroupId(1);
            return View(model);
        }
    }
}
