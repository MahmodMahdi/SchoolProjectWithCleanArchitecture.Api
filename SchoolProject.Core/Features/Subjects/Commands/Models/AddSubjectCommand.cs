using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Subjects.Commands.Models
{
	public class AddSubjectCommand : IRequest<Response<string>>
	{
		public string? NameAr { get; set; }
		public string? NameEn { get; set; }
		public int? Period { get; set; }
	}
}
