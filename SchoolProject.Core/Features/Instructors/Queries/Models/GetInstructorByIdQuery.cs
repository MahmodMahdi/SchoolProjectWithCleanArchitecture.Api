using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Dtos;

namespace SchoolProject.Core.Features.Instructors.Queries.Models
{
	public class GetInstructorByIdQuery : IRequest<Response<GetInstructorResponse>>
	{
		public int Id { get; set; }
	}
}
