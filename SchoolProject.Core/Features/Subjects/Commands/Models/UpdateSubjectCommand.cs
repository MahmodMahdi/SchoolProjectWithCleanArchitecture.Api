using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Subjects.Commands.Models
{
	public class UpdateSubjectCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public int? Period { get; set; }
	}
}
