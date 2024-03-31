using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Dtos;
namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
	public class GetInstructorOnlyByIdQuery : IRequest<Response<GetInstructorOnlyResponse>>
	{
		public int Id { get; set; }
		public GetInstructorOnlyByIdQuery(int id)
		{
			Id = id;
		}
	}
}
