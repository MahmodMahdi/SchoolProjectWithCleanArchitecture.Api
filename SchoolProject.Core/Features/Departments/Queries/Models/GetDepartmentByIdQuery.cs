using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Dtos;

namespace SchoolProject.Core.Features.Departments.Queries.Models
{
	public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentResponse>>
	{
		public int Id { get; set; }
		public int StudentPageNumber { get; set; }
		public int StudentPageSize { get; set; }
	}
}
