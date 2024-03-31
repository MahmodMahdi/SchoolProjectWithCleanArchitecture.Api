namespace SchoolProject.Core.Features.Students.Queries.Dtos
{
	public class GetStudentPaginatedListResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public int? Age { get; set; }
		public string? DepartmentName { get; set; }
		// with third way by mapping i don't need ctor 
		//public GetStudentPaginatedListResponse(int id, string name, string address, int? age, string departmentName)
		//{
		//	Id = id;
		//	Name = name;
		//	Address = address;
		//	Age = age;
		//	DepartmentName = departmentName;
		//}
	}
}
