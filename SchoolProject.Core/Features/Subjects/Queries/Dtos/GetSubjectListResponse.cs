namespace SchoolProject.Core.Features.Subjects.Queries.Dtos
{
	public class GetSubjectListResponse
	{
		public int SubjectID { get; set; }
		public string? Name { get; set; }
		public int? Period { get; set; }
	}
}
