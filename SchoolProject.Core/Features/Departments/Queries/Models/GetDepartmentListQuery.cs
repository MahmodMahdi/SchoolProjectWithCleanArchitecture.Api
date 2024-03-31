using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Dtos;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
	public class GetDepartmentListQuery : IRequest<Response<List<GetDepartmentListResponse>>>
	{
	}
}
