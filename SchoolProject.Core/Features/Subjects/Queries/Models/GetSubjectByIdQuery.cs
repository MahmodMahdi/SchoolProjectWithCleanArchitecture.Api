using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Queries.Dtos;

namespace SchoolProject.Core.Features.Subjects.Queries.Models
{
    public class GetSubjectByIdQuery : IRequest<Response<GetSubjectResponse>>
    {
        public int Id { get; set; }
        public GetSubjectByIdQuery(int id)
        {
            Id = id;
        }
    }
}
