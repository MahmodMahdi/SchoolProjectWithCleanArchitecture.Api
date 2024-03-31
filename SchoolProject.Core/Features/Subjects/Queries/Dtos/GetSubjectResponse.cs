namespace SchoolProject.Core.Features.Subjects.Queries.Dtos
{
	public class GetSubjectResponse
	{
		public int SubjectID { get; set; }
		public string? Name { get; set; }
		public int? Period { get; set; }
		public List<DepartmentSResponse>? DepartmentList { get; set; }
		public List<InstructorsResponse>? InstructorList { get; set; }
		public List<StudentSResponse>? StudentSList { get; set; }
	}
	public class StudentSResponse
	{
		public int Id { get; set; }
		public string? Name { get; }
	}
	public class DepartmentSResponse
	{
		public int Id { get; set; }
		public string? Name { get; }
	}
	public class InstructorsResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
