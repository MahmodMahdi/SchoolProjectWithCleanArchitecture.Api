using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Email.Commands.Models;
using SchoolProject.Data.Routing;

namespace SchoolProject.Api.Controllers
{
	[ApiController]
	[Authorize(Roles = "Admin,User")]
	public class EmailController : AppControllerBase
	{
		[HttpPost(Router.EmailRouting.SendEmail)]
		public async Task<IActionResult> Create([FromForm] SendEmailCommand command)
		{
			var result = NewResult(await _mediator.Send(command));
			return result;
		}
	}
}
