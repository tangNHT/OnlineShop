namespace WebApplication2.Models
{
	public class PaginationModel<T>
	{
		public IEnumerable<T> Items { get; set; }
		public int PageNumber { get; set; }
		public int TotalPages { get; set; }
		public int TotalItems { get; set; }
	}
}
