using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Features.Subjects.Queries.Models;
using SchoolProject.Data.Routing;

namespace SchoolProject.Api.Controllers
{
	[ApiController]

	public class SubjectController : AppControllerBase
	{
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.SubjectRouting.GetAll)]
		public async Task<IActionResult> GetSubjectList()
		{
			var subjects = NewResult(await _mediator.Send(new GetSubjectListQuery()));
			return subjects;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.SubjectRouting.GetSubjectOnlyById)]
		public async Task<IActionResult> GetSubjectOnlyById([FromRoute] int id)
		{
			var subject = NewResult(await _mediator.Send(new GetSubjectOnlyByIdQuery(id)));
			return subject;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.SubjectRouting.GetById)]
		public async Task<IActionResult> GetDepartmentById([FromRoute] int id)
		{
			var subject = NewResult(await _mediator.Send(new GetSubjectByIdQuery(id)));
			return subject;
		}
		[Authorize(Roles = "Admin")]
		[HttpPost(Router.SubjectRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddSubjectCommand command)
		{
			var subject = NewResult(await _mediator.Send(command));
			return subject;
		}
		[Authorize(Roles = "Admin")]
		[HttpPut(Router.SubjectRouting.Update)]
		public async Task<IActionResult> Update([FromBody] UpdateSubjectCommand command)
		{
			var subject = NewResult(await _mediator.Send(command));
			return subject;
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete(Router.SubjectRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var subject = NewResult(await _mediator.Send(new DeleteSubjectCommand(Id)));
			return subject;
		}
	}
}
