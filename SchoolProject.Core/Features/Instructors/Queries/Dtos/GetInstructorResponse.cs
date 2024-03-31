namespace SchoolProject.Core.Features.Instructors.Queries.Dtos
{
	public class GetInstructorResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? SuperVisor { get; set; }
		public string? Department { get; set; }
		public string? Image { get; set; }
		public string? DepartmentManager { get; set; }
		public List<InstructorsResponse>? SuperVisorOn { get; set; }
		public List<SubjectsResponse>? SubjectList { get; set; }
	}
	// attention
	public class InstructorsResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
	public class SubjectsResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
	}
}
