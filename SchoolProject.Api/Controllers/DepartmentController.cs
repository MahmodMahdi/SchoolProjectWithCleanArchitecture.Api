using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Data.Routing;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class DepartmentController : AppControllerBase
	{
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.DepartmentRouting.GetAll)]
		public async Task<IActionResult> GetDepartmentList()
		{
			var departments = NewResult(await _mediator.Send(new GetDepartmentListQuery()));
			return departments;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.DepartmentRouting.GetById)]
		public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
		{
			var department = NewResult(await _mediator.Send(query));
			return department;
		}
		[Authorize(Roles = "Admin,User")]
		[HttpGet(Router.DepartmentRouting.GetDepartmentOnlyById)]
		public async Task<IActionResult> GetDepartmentOnly([FromRoute] int id)
		{
			var department = NewResult(await _mediator.Send(new GetDepartmentOnlyByIdQuery(id)));
			return department;
		}
		[Authorize(Policy = "CreateDepartment")]
		[HttpPost(Router.DepartmentRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddDepartmentCommand command)
		{
			var department = NewResult(await _mediator.Send(command));
			return department;
		}
		[Authorize(Policy = "UpdateDepartment")]
		[HttpPut(Router.DepartmentRouting.Update)]
		public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand command)
		{
			var department = NewResult(await _mediator.Send(command));
			return department;
		}
		[Authorize(Policy = "DeleteDepartment")]
		[HttpDelete(Router.DepartmentRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var department = NewResult(await _mediator.Send(new DeleteDepartmentCommand(Id)));
			return department;
		}
	}
}
