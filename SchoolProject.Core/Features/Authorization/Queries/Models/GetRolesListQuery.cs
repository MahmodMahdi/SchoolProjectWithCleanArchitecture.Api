using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Dtos;

namespace SchoolProject.Core.Features.Authorization.Queries.Models
{
	public class GetRolesListQuery : IRequest<Response<List<GetRolesListResponse>>>
	{
	}
}
