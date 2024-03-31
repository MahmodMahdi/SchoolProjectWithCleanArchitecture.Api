namespace SchoolProject.Core.Features.Subjects.Queries.Dtos
{
	public class GetSubjectOnlyResponse
	{
		public int SubjectID { get; set; }
		public string? Name { get; set; }
		public int? Period { get; set; }
	}
}
