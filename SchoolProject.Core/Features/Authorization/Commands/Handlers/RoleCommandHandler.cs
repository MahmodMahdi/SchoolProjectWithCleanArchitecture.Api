using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Commands.Handlers
{
	public class RoleCommandHandler : ResponseHandler,
										IRequestHandler<AddRoleCommand, Response<string>>,
										IRequestHandler<UpdateRoleCommand, Response<string>>,
										IRequestHandler<DeleteRoleCommand, Response<string>>,
										IRequestHandler<UpdateUserRolesCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly IAuthorizationService _authorizationService;


		#endregion
		#region Constructor
		public RoleCommandHandler(
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
		public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.AddRoleAsync(request.RoleName!);
			if (result == "Success") return Success(result);
			return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
		}

		public async Task<Response<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateRoleAsync(request);
			if (result == "NotFound") return NotFound<string>();
			else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
			else return BadRequest<string>(result);
		}

		public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.DeleteRoleAsync(request.Id);
			if (result == "NotFound") return NotFound<string>();
			if (result == "RoleIsUsed") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
			else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
			else return BadRequest<string>(result);
		}

		public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
		{
			var result = await _authorizationService.UpdateUserRoles(request);
			switch (result)
			{
				case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
				case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
				case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
				case "FailedToUpdateUserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
			}
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
		}
		#endregion
	}
}
