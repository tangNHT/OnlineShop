using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
	public class CategoryModel
	{
		private OnlineShopDbContext context;
		public CategoryModel()
		{
			var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
			var options = optionsBuilder.Options;
			context = new OnlineShopDbContext(options);
		}
		public List<Category> ListAll()
		{
			var listCategory = context.Categories.FromSqlRaw("EXEC Sp_Category_ListAll").ToList();
			return listCategory;
		}
	}
}
