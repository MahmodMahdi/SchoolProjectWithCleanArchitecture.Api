using MediatR;
using SchoolProject.Core.Bases;

namespace SchoolProject.Core.Features.Instructors.Commands.Models
{
	public class DeleteInstructorCommand : IRequest<Response<string>>
	{
		public int Id { get; set; }
		public DeleteInstructorCommand(int id)
		{
			Id = id;
		}
	}
}
