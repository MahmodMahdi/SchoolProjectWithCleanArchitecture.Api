﻿
namespace SchoolProject.Core.Features.Students.Queries.Dtos
{
	public class GetStudentListResponse
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Address { get; set; }
		public string? Department { get; set; }
	}
}
