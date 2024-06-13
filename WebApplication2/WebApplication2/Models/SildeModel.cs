using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
	public class SildeModel
	{
		private OnlineShopDbContext context;
		public SildeModel()
		{
			var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
			var options = optionsBuilder.Options;
			context = new OnlineShopDbContext(options);
		}

		public List<Silde> ListAll()
		{
			return (from s in context.Sildes where s.Status == true orderby s.DispalyOrder select s).ToList();
		}
	}
}
