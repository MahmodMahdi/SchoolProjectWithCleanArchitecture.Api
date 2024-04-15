using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Dtos;
using SchoolProject.Data.Routing;
using Swashbuckle.AspNetCore.Annotations;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	public class AuthenticationController : AppControllerBase
	{
		[HttpPost(Router.AuthenticationRouting.SignIn)]
		public async Task<IActionResult> SignIn([FromForm] SignInCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpPost(Router.AuthenticationRouting.RefreshToken)]
		public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[Authorize(Roles = "Admin")]
		[HttpPost(Router.AuthenticationRouting.ValidateToken)]
		public async Task<IActionResult> ValidateToken([FromForm] AuthorizeUserQuery command)
		{
			var user = NewResult(await _mediator.Send(command));
			return user;
		}
		[HttpGet(Router.AuthenticationRouting.ConfirmEmail)]
		public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailQuery query)
		{
			var email = NewResult(await _mediator.Send(query));
			return email;
		}
		[SwaggerOperation(Summary = " تأكيد كود الباسورد ", OperationId = "ConfirmResetPassword")]
		[HttpGet(Router.AuthenticationRouting.ConfirmResetPassword)]
		public async Task<IActionResult> ConfirmResetPassword([FromQuery] ConfirmResetPasswordQuery query)
		{
			var response = NewResult(await _mediator.Send(query));
			return response;
		}
		[SwaggerOperation(Summary = "إرسال كود تأكيد الباسورد", OperationId = "SendResetPasswordCode")]
		[HttpPost(Router.AuthenticationRouting.SendResetPasswordCode)]
		public async Task<IActionResult> SendResetPasswordCode([FromQuery] SendResetPasswordCommand command)
		{
			var response = NewResult(await _mediator.Send(command));
			return response;
		}
		[SwaggerOperation(Summary = " تغيير الباسورد ", OperationId = "ResetPassword")]
		[HttpPost(Router.AuthenticationRouting.ResetPassword)]
		public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
		{
			var response = NewResult(await _mediator.Send(command));
			return response;
		}
	}
}
