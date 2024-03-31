using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Data.Routing;

namespace SchoolProject.Api.Controllers
{
	[ApiController]

	public class ApplicationUserController : AppControllerBase
	{
		// this mean it allow for any one to register
		[AllowAnonymous]
		[HttpPost(Router.UserRouting.Create)]
		public async Task<IActionResult> Create([FromBody] AddUserCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpGet(Router.UserRouting.Paginated)]
		public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
		{
			var user = Ok(await _mediator.Send(query));
			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpGet(Router.UserRouting.GetById)]
		public async Task<IActionResult> GetUserById([FromRoute] int id)
		{
			var user = NewResult(await _mediator.Send(new GetUserByIdQuery(id)));
			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpPut(Router.UserRouting.Update)]
		public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete(Router.UserRouting.Delete)]
		public async Task<IActionResult> Delete([FromRoute] int Id)
		{
			var user = NewResult(await _mediator.Send(new DeleteUserCommand(Id)));
			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpPut(Router.UserRouting.ChangePassword)]
		public async Task<IActionResult> Update([FromBody] ChangeUserPasswordCommand command)
		{
			var ChangePassword = NewResult(await _mediator.Send(command));
			return ChangePassword;
		}
	}
}
