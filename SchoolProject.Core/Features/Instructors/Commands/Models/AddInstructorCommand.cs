using MediatR;
using Microsoft.AspNetCore.Http;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
	public class AddInstructorCommand : IRequest<Response<string>>
	{
		public string? InstructorNameAr { get; set; }
		public string? InstructorNameEn { get; set; }
		public string? Address { get; set; }
		public string? Phone { get; set; }
		public decimal? Salary { get; set; }
		public IFormFile? Image { get; set; }
		public int? SuperVisorId { get; set; }
		public int? DepartmentId { get; set; }
	}
}
