using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
	internal class ClaimsCommandHandler : ResponseHandler,
										IRequestHandler<UpdateUserClaimsCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly IAuthorizationService _authorizationService;


		#endregion
		#region Constructor
		public ClaimsCommandHandler(
								  IStringLocalizer<SharedResources> stringLocalizer,
								  UserManager<User> userManager,
								  IAuthorizationService authorizationService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_userManager = userManager;
			_authorizationService = authorizationService;
		}


		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateUserClaims(request);
			switch (result)
			{
				case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
				case "FailedToRemoveOldClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldClaims]);
				case "FailedToAddNewClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewClaims]);
				case "FailedToUpdateUserClaims": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserClaims]);
			}
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
		}
		#endregion
	}
}
