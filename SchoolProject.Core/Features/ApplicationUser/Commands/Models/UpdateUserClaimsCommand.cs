using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Models
{
	public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
	{
	}
}
