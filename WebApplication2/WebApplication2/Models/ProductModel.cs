using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
	public class ProductModel
	{
		private OnlineShopDbContext context;
		public ProductModel()
		{
			var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
			var options = optionsBuilder.Options;
			context = new OnlineShopDbContext(options);
		}

		public List<Product> ListNewProduct(int top)
		{
			return (from p in  context.Products orderby p.CreatedDate descending select p).Take(top).ToList();
		}
		public List<Product> ListFeatureProduct(int top)
		{
			return (from p in context.Products where p.TopHot != null  orderby p.CreatedDate descending select p).Take(top).ToList();
		}
	}
}
