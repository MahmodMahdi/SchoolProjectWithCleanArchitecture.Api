using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
	public class SignInCommand : IRequest<Response<JwtAuthenticationResponse>>
	{
		public string? Email { get; set; }
		public string? Password { get; set; }
	}
}
