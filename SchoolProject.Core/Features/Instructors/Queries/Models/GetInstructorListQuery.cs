using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Dtos;
namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
	public class GetInstructorListQuery : IRequest<Response<List<GetInstructorListResponse>>>
	{

	}
}
