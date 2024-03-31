using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.Routing;

namespace SchoolProject.Api.Controllers
{

	public class StudentController : AppControllerBase
	{
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.StudentRouting.GetAll)]
		public async Task<IActionResult> GetStudentList()
		{
			var students = NewResult(await _mediator.Send(new GetStudentListQuery()));
			return students;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.StudentRouting.Paginated)]
		public async Task<IActionResult> Paginated([FromQuery] GetStudentPaginatedListQuery query)
		{
			var student = Ok(await _mediator.Send(query));
			return student;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.StudentRouting.GetById)]
		public async Task<IActionResult> GetStudentById([FromRoute] int id)
		{
			var student = NewResult(await _mediator.Send(new GetStudentByIdQuery(id)));
			return student;
		}
		[Authorize(Roles = "Admin")]
		[HttpPost(Router.StudentRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddStudentCommand command)
		{
			var student = NewResult(await _mediator.Send(command));
			return student;
		}
		[Authorize(Roles = "Admin")]
		[HttpPut(Router.StudentRouting.Update)]
		public async Task<IActionResult> Edit([FromBody] UpdateStudentCommand command)
		{
			var student = NewResult(await _mediator.Send(command));
			return student;
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete(Router.StudentRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var student = NewResult(await _mediator.Send(new DeleteStudentCommand(Id)));
			return student;
		}
	}
}
