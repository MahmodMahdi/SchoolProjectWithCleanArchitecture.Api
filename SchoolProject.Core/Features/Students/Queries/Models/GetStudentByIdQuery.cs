using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Dtos;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
	public class GetStudentByIdQuery : IRequest<Response<GetStudentResponse>>
	{
		public int Id { get; set; }
        public GetStudentByIdQuery(int id)
        {
            Id = id;
        }
    }
}
