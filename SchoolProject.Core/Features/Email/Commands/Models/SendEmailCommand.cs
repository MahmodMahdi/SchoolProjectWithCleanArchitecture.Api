using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Email.Commands.Models
{
	public class SendEmailCommand : IRequest<Response<string>>
	{
		public string Email { get; set; }
		public string Message { get; set; }
	}
}
