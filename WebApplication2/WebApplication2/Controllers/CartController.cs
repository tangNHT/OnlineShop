using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication2.Models;
using WebApplication2.Common;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
namespace WebApplication2.Controllers
{
	public class CartController : Controller
	{
        private readonly EmailSettings _emailSettings;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public CartController(IOptions<EmailSettings> emailSettings, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _emailSettings = emailSettings.Value;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        List<CartItem>? sessionCart = new List<CartItem>();

        public IActionResult Index()
		{
            sessionCart = GetSessionCart();

            return View(sessionCart);
		}

		public ActionResult AddItem(int productId, int quantity)
		{
			var product = new ProductModel().ViewDetail(productId);
			if (sessionCart != null)
			{
                sessionCart = GetSessionCart();
				//Nếu đã tồn tại thì tăng số lượng
				if (sessionCart.Exists(x=>x.Product.Id == productId))
				{
					foreach(var item in sessionCart)
					{
						if(item.Product.Id == productId)
						{
							item.Quantity += quantity;
						}
					}
				}
				//Nếu chưa tồn tại thì tạo thêm một đối tượng CartItem
				else
				{
                    AddItemSessionCart(quantity, product);

                }
				//Chuyển đổi listCart thành chuỗi JSON
				//Lưu chuối JSON vào session
				SetSessionCart();
			}
			else
            {
                AddItemSessionCart(quantity, product);

                //Chuyển đổi listCart thành chuối JSON
                //Lưu chuỗi JSON vào session
                SetSessionCart();
            }
            return RedirectToAction("Index");
		}

        private void AddItemSessionCart(int quantity, Product product)
        {
            //Tạo mới một CartItem
            var item = new CartItem();
            item.Product = product;
            item.Quantity = quantity;

            //Tạo một list CartItem
            sessionCart.Add(item);
        }

        public JsonResult Update(string cartModel)
		{
			var jsonCart = GetJsonString(cartModel);
            sessionCart = GetSessionCart();
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.Id == item.Product.Id);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
			SetSessionCart();
            return Json(new
			{
				status = true
			});
		}

		public JsonResult DeleteAll(string cartModel)
		{
			HttpContext.Session.SetString(CommonConstants.CartSession,string.Empty);
			return Json(new
			{
				status = true
			});
		}

		public JsonResult Delete(int id)
        {
            sessionCart = GetSessionCart();
            sessionCart.RemoveAll(x => x.Product.Id == id);
            SetSessionCart();
            return Json(new
            {
                status = true
            });
        }

        private void SetSessionCart()
        {
            HttpContext.Session.SetString(CommonConstants.CartSession,
                            JsonConvert.SerializeObject(sessionCart));
        }

        private List<CartItem>? GetSessionCart()
        {
            var jsonSessionCart = HttpContext.Session.GetString(CommonConstants.CartSession);
            var sessionCart = new List<CartItem>();
            if (!string.IsNullOrEmpty(jsonSessionCart))
            {
                sessionCart = GetJsonString(jsonSessionCart);
            }

            return sessionCart;
        }

        private List<CartItem>? GetJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<List<CartItem>>(jsonString);
        }

		public ActionResult Payment()
		{
            sessionCart = GetSessionCart();
            return View(sessionCart);
		}

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipEmail = email;
            order.ShipName = shipName;
            order.ShipMobile = mobile;

            try
            {
                var id = new OrderModel().Insert(order);
                sessionCart = GetSessionCart();
                var detail = new OrderDetailModel();
                decimal total = 0;
                foreach (var item in sessionCart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductId = item.Product.Id;
                    orderDetail.OrderId = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    var result = detail.Insert(orderDetail);
                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                    if (result)
                    {
                        TempData["SuccessMessage"] = "Mua hàng thành công";
                        HttpContext.Session.SetString(CommonConstants.CartSession, string.Empty);
                        TempData["RedirectUrl"] = Url.Action("Index","Home");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Mua hàng không thành công";
                    }

                    var toEmail = _emailSettings.ToEmailAddress;

                    SendMail(email, "Đơn hàng mới từ OnlineShop");
                    SendMail(toEmail, "Đơn hàng mới từ OnlineShop");
                }
            }
            catch
            {
                throw;
            }
            return View(sessionCart);
        }

        public async void SendMail(string toEmailAddress, string subject)
        {
            var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();

            // Đường dẫn tới tệp HTML
            string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Client", "template", "neworder.html");

            // Đọc nội dung của tệp HTML
            string emailBody;
            using (StreamReader reader = new StreamReader(filePath))
            {
                emailBody = await reader.ReadToEndAsync();
            }

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailSettings.FromEmailDisplayName, emailSettings.FromEmailAddress));
            emailMessage.To.Add(new MailboxAddress("", toEmailAddress));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = emailBody };

            using (var client = new SmtpClient())
            {
                // Bỏ qua kiểm tra chứng chỉ
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(emailSettings.SMTPHost, emailSettings.SMTPPort, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(emailSettings.FromEmailAddress, emailSettings.FromEmailPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
		}
    }
}
