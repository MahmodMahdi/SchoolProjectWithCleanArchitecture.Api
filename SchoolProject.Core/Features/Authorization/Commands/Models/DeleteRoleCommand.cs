using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Requests;

namespace SchoolProject.Core.Features.Authorization.Commands.Models
{
	public class DeleteRoleCommand : UpdateRoleRequest, IRequest<Response<string>>
	{
		//public  int Id { get; set; }
		public DeleteRoleCommand(int id)
		{
			Id = id;
		}
	}
}
