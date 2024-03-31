using MediatR;
using SchoolProject.Core.Bases;
namespace SchoolProject.Core.Features.Departments.Commands.Models
{
	public class UpdateDepartmentCommand : IRequest<Response<string>>
	{
		public int DepartmentId { get; set; }
		public string? DepartmentNameAr { get; set; }
		public string? DepartmentNameEn { get; set; }
		public int DepartmentManager { get; set; }
	}
}
