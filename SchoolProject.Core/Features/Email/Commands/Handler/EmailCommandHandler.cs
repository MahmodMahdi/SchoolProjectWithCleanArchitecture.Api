using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Email.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Email.Commands.Handler
{
	public class EmailCommandHandler : ResponseHandler,
										IRequestHandler<SendEmailCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly IEmailService _emailService;


		#endregion
		#region Constructor
		public EmailCommandHandler(
								  IStringLocalizer<SharedResources> stringLocalizer,
								  IEmailService emailService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_emailService = emailService;
		}


		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
		{
			var result = await _emailService.SendEmailAsync(request.Email, request.Message, null!);
			if (result == "Success")
			{
				return Success<string>("");
			}
			return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SendEmailFailed]);
		}

		#endregion
	}
}
