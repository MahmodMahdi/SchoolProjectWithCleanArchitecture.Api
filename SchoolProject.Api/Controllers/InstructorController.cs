using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Data.Routing;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class InstructorController : AppControllerBase
	{
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.InstructorRouting.GetAll)]
		public async Task<IActionResult> GetInstructorList()
		{
			var instructors = NewResult(await _mediator.Send(new GetInstructorListQuery()));
			return instructors;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.InstructorRouting.GetById)]
		public async Task<IActionResult> GetInstructorById([FromRoute] int id)
		{
			var instructor = NewResult(await _mediator.Send(new GetInstructorByIdQuery() { Id = id }));
			return instructor;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.InstructorRouting.GetInstructorOnlyById)]
		public async Task<IActionResult> GetInstructorOnly([FromRoute] int id)
		{
			var instructor = NewResult(await _mediator.Send(new GetInstructorOnlyByIdQuery(id)));
			return instructor;
		}
		[Authorize(Policy = "CreateInstructor")]
		[HttpPost(Router.InstructorRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddInstructorCommand command)
		{
			var instructor = NewResult(await _mediator.Send(command));
			return instructor;
		}
		[Authorize(Policy = "UpdateInstructor")]
		[HttpPut(Router.InstructorRouting.Update)]
		public async Task<IActionResult> Update([FromForm] UpdateInstructorCommand command)
		{
			var instructor = NewResult(await _mediator.Send(command));
			return instructor;
		}
		[Authorize(Policy = "DeleteInstructor")]
		[HttpDelete(Router.InstructorRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var instructor = NewResult(await _mediator.Send(new DeleteInstructorCommand(Id)));
			return instructor;
		}
	}
}
