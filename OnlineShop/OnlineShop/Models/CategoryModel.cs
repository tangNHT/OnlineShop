using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
		public IEnumerable<Category> ListAll(int pageNumber, int pageSize)
		{
			//Danh sách sắp xếp theo ID, bỏ qua số bản ghi dựa trên số trang và hiển thị số bản ghi
			var categories = context.Categories.OrderByDescending(c => c.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			foreach (var category in typeof(Category).GetProperties()) { 
			}
			return categories;
		}
		public int TotalPages( int pageSize)
		{
			//Tổng số trang
			return (int)Math.Ceiling(context.Categories.Count() / (double)pageSize); ;
		}
		public int TotalItems()
		{
			//Số lượng bản ghi
			return context.Categories.Count();
		}
		public bool Update(Category category)
		{
			try
			{
				var user = context.Categories.Find(category.Id);
				user.Name = category.Name;
				user.Alias = category.Alias;
				user.ParentId = category.ParentId;
				user.CreateDate = DateTime.Now;
				user.Order = category.Order;
				user.Status = category.Status;
				context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}
		public Category ViewDetail(int id)
		{
			return context.Categories.Find(id);
		}
		public async Task<int> Create(string name, string alias, int? parentID, int? order, bool? status)
		{
			// Tạo tham số output cho ID mới
			var newIdParameter = new SqlParameter
			{
				ParameterName = "@NewID",
				SqlDbType = System.Data.SqlDbType.Int,
				Direction = System.Data.ParameterDirection.Output //Nhận giá trị ID mới được tạo từ stored procedure
			};

			// Câu lệnh SQL để gọi stored procedure
			var sql = "EXEC Sp_Category_Insert @Name, @Alias, @ParentID, @Order, @Status, @NewID OUTPUT";

			// Gọi stored procedure với các tham số
			await context.Database.ExecuteSqlRawAsync(sql,
				new SqlParameter("@Name", name),
				new SqlParameter("@Alias", alias),
				new SqlParameter("@ParentID", parentID ?? (object)DBNull.Value), //Xử lý các giá trị nulll
				new SqlParameter("@Order", order ?? (object)DBNull.Value),
				new SqlParameter("@Status", status ?? (object)DBNull.Value),
				newIdParameter);

			// Trả về giá trị ID mới được chèn
			return (int)newIdParameter.Value;
		}

	}
}
