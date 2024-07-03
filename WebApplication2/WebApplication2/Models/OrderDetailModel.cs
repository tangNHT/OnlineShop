using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    public class OrderDetailModel
    {
        private OnlineShopDbContext context;
        public OrderDetailModel()
        {
            var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
            var options = optionsBuilder.Options;
            context = new OnlineShopDbContext(options);
        }
        public bool Insert(OrderDetail orderDetail)
        {
            context.OrderDetails.Add(orderDetail);
            context.SaveChanges();
            return true;
        }
    }
}
