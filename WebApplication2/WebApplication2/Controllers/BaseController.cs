using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication2.Common;

public class BaseController : Controller
{
	// Ghi đè phương thức OnActionExecuting để thực hiện kiểm tra trước khi bất kỳ action nào trong controller được thực thi
	public override void OnActionExecuting(ActionExecutingContext filterContext)
	{
		// Lấy giá trị session từ HttpContext
		var session = HttpContext.Session.GetString(CommonConstants.USER_SESSION);
		// Kiểm tra người dùng đã đăng nhập chưa
		if (session == null)
		{
			// Chuyển hướng người dùng đến trang đăng nhập
			filterContext.Result = new RedirectToRouteResult(
				new RouteValueDictionary(
					new { controller = "Login", action = "Index" }
				)
			);
		}
		base.OnActionExecuting(filterContext);
	}
}
