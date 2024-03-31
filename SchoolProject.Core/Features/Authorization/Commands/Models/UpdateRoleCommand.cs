using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
    public class UpdateRoleCommand : UpdateRoleRequest, IRequest<Response<string>>
	{

	}
}
