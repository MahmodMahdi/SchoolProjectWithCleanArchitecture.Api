using MediatR;
using SchoolProject.Core.Bases;
namespace SchoolProject.Core.Features.Departments.Commands.Models
{
	public class DeleteDepartmentCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public DeleteDepartmentCommand(int id)
		{
			Id = id;
		}
	}
}
