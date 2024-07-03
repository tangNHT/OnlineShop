using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace WebApplication2.Models
{
    public class OrderModel
    {
        private OnlineShopDbContext context;
        public OrderModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
            var options = optionsBuilder.Options;
            context = new OnlineShopDbContext(options);
        }
        public int Insert(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order.Id;
        }
    }
}
