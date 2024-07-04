using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
	public class ContactModel
	{
		private OnlineShopDbContext context;
		public ContactModel()
		{
			var optionsBuilder = new DbContextOptionsBuilder<OnlineShopDbContext>();
			optionsBuilder.UseSqlServer("Server=DESKTOP-AF7B9H9\\ARCTECDATABASE2;Database=OnlineShop;User ID=sa;Password=administrator;Integrated Security=True;Encrypt=False;");
			var options = optionsBuilder.Options;
			context = new OnlineShopDbContext(options);
		}

		public Contact GetActiveContact()
		{
			return context.Contacts.Single(x => x.Status == 1);
		}
	}
}
