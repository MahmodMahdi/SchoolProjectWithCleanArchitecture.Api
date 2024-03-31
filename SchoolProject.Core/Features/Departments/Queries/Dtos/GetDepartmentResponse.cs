using SchoolProject.Core.Wrappers;

namespace SchoolProject.Core.Features.Departments.Queries.Dtos
{
	public class GetDepartmentResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? ManagerName { get; set; }
		public PaginateResult<StudentResponse>? StudentList { get; set; }
		public List<SubjectResponse>? SubjectList { get; set; }
		public List<InstructorResponse>? InstructorList { get; set; }
	}
	public class StudentResponse
	{
		public int ID { get; set; }
		public string? Name { get; set; }
		public StudentResponse(int id, string name)
		{
			ID = id;
			Name = name;
		}
	}
	public class SubjectResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
	public class InstructorResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
