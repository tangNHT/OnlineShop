using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký cấu hình EmailSettings từ appsettings.jso
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddResponseCaching(); // Thêm dịch vụ caching

//Cấu hình dịch vụ session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); //Thời gian timeout của session
    options.Cookie.HttpOnly = true; //Chỉ có thể truy cập sesstion thông qua http
    options.Cookie.IsEssential = true;  // Đảm bảo cookie của session luôn được gửi
});

// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware để bỏ qua các yêu cầu đến BotDetectCaptcha.ashx
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Contains("BotDetectCaptcha.ashx"))
    {
        context.Response.StatusCode = 404;
        return;
    }
    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseResponseCaching(); // Bật middleware caching

app.UseAuthorization();

app.MapControllerRoute(
    name: "Product Category",
    pattern: "san-pham/{metatitle}-{cateId}",
    defaults: new { controller = "Product", action = "ProductCategory" });

app.MapControllerRoute(
    name: "Product Detail",
    pattern: "chi-tiet/{metatitle}-{id}",
    defaults: new { controller = "Product", action = "Detail" });

app.MapControllerRoute(
    name: "Add Cart",
    pattern: "them-gio-hang",
    defaults: new { controller = "Cart", action = "AddItem" });

app.MapControllerRoute(
    name: "Cart",
    pattern: "gio-hang",
    defaults: new { controller = "Cart", action = "Index" });

app.MapControllerRoute(
    name: "Payment",
    pattern: "thanh-toan",
    defaults: new { controller = "Cart", action = "Payment" });

app.MapControllerRoute(
    name: "Contact",
    pattern: "lien-he",
    defaults: new { controller = "Contact", action = "Index" });

app.MapControllerRoute(
	name: "Register",
	pattern: "dang-ky",
	defaults: new { controller = "User", action = "Register" });

app.MapControllerRoute(
    name: "Search",
    pattern: "tim-kiem",
    defaults: new { controller = "Search", action = "Product" });

app.MapControllerRoute(
    name: "Login",
    pattern: "dang-nhap",
    defaults: new { controller = "User", action = "Login" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();