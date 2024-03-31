using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Queries.Dtos;

namespace SchoolProject.Core.Features.Subjects.Queries.Models
{
	public class GetSubjectListQuery : IRequest<Response<List<GetSubjectListResponse>>>
	{
	}
}
