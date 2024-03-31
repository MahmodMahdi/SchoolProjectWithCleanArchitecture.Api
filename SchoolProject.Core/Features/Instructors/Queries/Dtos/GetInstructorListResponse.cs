namespace SchoolProject.Core.Features.Instructors.Queries.Dtos
{
	public class GetInstructorListResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? SuperVisor { get; set; }
		public string? Image { get; set; }
		public string? Department { get; set; }
	}
}
