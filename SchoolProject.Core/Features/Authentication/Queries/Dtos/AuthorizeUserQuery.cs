using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Authentication.Queries.Dtos
{
	public class AuthorizeUserQuery : IRequest<Response<string>>
	{
		public string? AccessToken { get; set; }
	}
}
