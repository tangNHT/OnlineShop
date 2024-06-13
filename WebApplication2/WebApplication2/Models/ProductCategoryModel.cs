using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
	public class ProductCategoryModel
	{
		private OnlineShopDbContext context;
		public ProductCategoryModel()
		{
			var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
			var options = optionsBuilder.Options;
			context = new OnlineShopDbContext(options);
		}

		public List<ProductCategory> ListAll()
		{
			return (from n in context.ProductCategories where n.Status == true orderby n.DisplayOrder select n).ToList();
		}
	}
}
