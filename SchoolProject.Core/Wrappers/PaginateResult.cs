namespace SchoolProject.Core.Wrappers
{
	public class PaginateResult<T>
	{
		public PaginateResult(List<T> data)
		{
			Data = data;
		}
		internal PaginateResult(bool succeeded, List<T>? data = default, List<string>? messages = null, int count = 0, int page = 1, int pageSize = 10)
		{
			Data = data;
			CurrentPage = page;
			Succeeded = succeeded;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(count / (double)pageSize);
			TotalCount = count;
		}
		public static PaginateResult<T> Success(List<T> data, int count, int page, int pageSize)
		{
			return new(true, data, null, count, page, pageSize);
		}
		public List<T>? Data { get; set; }
		public int CurrentPage { get; set; }
		public object? Meta { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
		public int TotalCount { get; set; }
		public bool HasPreviousPage => CurrentPage > 1;
		public bool HasNextPage => CurrentPage < TotalPages;
		public List<string> Messages { get; set; } = new();
		public bool Succeeded { get; set; }
	}
}
