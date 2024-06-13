using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Models
{
	public class MenuModel
	{
		private OnlineShopDbContext context;
		public MenuModel()
		{
			var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
			var options = optionsBuilder.Options;
			context = new OnlineShopDbContext(options);
		}

		public List<Menu> ListByGroupId(int groupId)
		{
			return (from n in context.Menus where n.TypeId == groupId orderby n.DisplayOrder select n).ToList(); ;
		}
	}
}
