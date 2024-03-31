using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authentication.Commands.Models
{
	public class RefreshTokenCommand : IRequest<Response<JwtAuthenticationResponse>>
	{
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
	}
}
