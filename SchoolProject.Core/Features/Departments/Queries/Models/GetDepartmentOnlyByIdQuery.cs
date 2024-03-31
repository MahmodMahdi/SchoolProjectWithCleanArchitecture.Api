using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Dtos;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
	public class GetDepartmentOnlyByIdQuery : IRequest<Response<GetDepartmentOnlyResponse>>
	{
		public int Id { get; set; }
		public GetDepartmentOnlyByIdQuery(int id)
		{
			Id = id;
		}
	}
}
