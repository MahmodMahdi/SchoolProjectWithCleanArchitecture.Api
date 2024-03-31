using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Data.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin")]
	public class AuthorizationController : AppControllerBase
	{
		[HttpGet(Router.AuthorizationRouting.GetAll)]
		public async Task<IActionResult> GetRoleList()
		{
			var role = NewResult(await _mediator.Send(new GetRolesListQuery()));
			return role;
		}
		[HttpGet(Router.AuthorizationRouting.GetById)]
		public async Task<IActionResult> GetRole([FromRoute] int id)
		{
			var role = NewResult(await _mediator.Send(new GetRoleByIdQuery(id)));
			return role;
		}
		[HttpPost(Router.AuthorizationRouting.Create)]
		public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
		{
			var role = NewResult(await _mediator.Send(command));
			return role;
		}
		[HttpPut(Router.AuthorizationRouting.Update)]
		public async Task<IActionResult> Update([FromForm] UpdateRoleCommand command)
		{
			var role = NewResult(await _mediator.Send(command));
			return role;
		}
		[HttpDelete(Router.AuthorizationRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int id)
		{
			var role = NewResult(await _mediator.Send(new DeleteRoleCommand(id)));
			return role;
		}

		[SwaggerOperation(Summary = "إدارة صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
		[HttpGet(Router.AuthorizationRouting.ManageUserRoles)]
		public async Task<IActionResult> ManageUserRoles([FromRoute] int userId)
		{
			var result = NewResult(await _mediator.Send(new ManageUserRoleQuery(userId)));
			return result;
		}


		[SwaggerOperation(Summary = "إدارة التحكم في صلاحيات المستخدمين", OperationId = "ManageUserClaims")]
		[HttpGet(Router.AuthorizationRouting.ManageUserClaims)]
		public async Task<IActionResult> ManageUserClaims([FromRoute] int userId)
		{
			var result = NewResult(await _mediator.Send(new ManageUserClaimsQuery(userId)));
			return result;
		}

		[SwaggerOperation(Summary = "تعديل صلاحيات المستخدم", OperationId = "UpdateUserRoles")]
		[HttpPut(Router.AuthorizationRouting.UpdateUserRoles)]
		public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
		[SwaggerOperation(Summary = "تعديل صلاحيات الإستخدام للمستخدم", OperationId = "UpdateUserClaims")]
		[HttpPut(Router.AuthorizationRouting.UpdateUserClaims)]
		public async Task<IActionResult> UpdateUserClaims([FromBody] UpdateUserClaimsCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
	}
}
