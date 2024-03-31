using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Data.Responses;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
    public class ManageUserRoleQuery : IRequest<Response<ManageUserRoleResponse>>
	{
		public int UserId { get; set; }
		public ManageUserRoleQuery(int id)
		{
			UserId = id;
		}
	}
}
