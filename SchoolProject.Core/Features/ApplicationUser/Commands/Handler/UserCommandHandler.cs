using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.ApplicationUser.Commands.Handler
{
	public class UserCommandHandler : ResponseHandler,
		IRequestHandler<AddUserCommand, Response<string>>,
		IRequestHandler<UpdateUserCommand, Response<string>>,
		IRequestHandler<DeleteUserCommand, Response<string>>,
		IRequestHandler<ChangeUserPasswordCommand, Response<string>>
	{

		#region Fields
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly IApplicationUserService _applicationUserService;


		#endregion
		#region Constructor
		public UserCommandHandler(IMapper mapper,
								  IStringLocalizer<SharedResources> stringLocalizer,
								  UserManager<User> userManager,
								   IApplicationUserService applicationUserService) : base(stringLocalizer)
		{
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
			_userManager = userManager;

			_applicationUserService = applicationUserService;
		}
		#endregion
		#region Handle Function
		public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
		{
			//Mapping
			var IdentityUser = _mapper.Map<User>(request);

			//Check on Phone Number (can't duplicate)
			var SearchByPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == IdentityUser.PhoneNumber);
			if (SearchByPhone != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PhoneExist]);

			var Result = await _applicationUserService.AddUserAsync(IdentityUser, request.Password!);
			// Failed
			switch (Result)
			{
				case "EmailIsExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.EmailIsExist]);
				case "PhoneExist": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PhoneExist]);
				case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
				case "Success": return Success<string>("");
				default: return BadRequest<string>(Result);
			}
		}

		public async Task<Response<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			// Check if user exist 
			// var user = await _userManager.FindByIdAsync(request.Id.ToString());
			var existUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

			// Not exist
			if (existUser == null) return NotFound<string>();

			// mapping
			var newUser = _mapper.Map(request, existUser);

			//Check on Phone Number (can't dublicate)
			var SearchByPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == newUser.PhoneNumber && x.Id != newUser.Id);
			if (SearchByPhone != null) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.PhoneExist]);

			//update
			var result = await _userManager.UpdateAsync(newUser);

			//Not success 
			if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UpdatedFailed]);

			//Success messege
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Updated]);
		}

		public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
		{
			// Check if user exist 
			// var user = await _userManager.FindByIdAsync(request.Id.ToString());
			var User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

			// Not exist
			if (User == null) return NotFound<string>();

			// Delete the item
			var result = await _userManager.DeleteAsync(User);

			//Not success 
			if (!result.Succeeded) return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.DeletedFailed]);

			//Success message
			return Success<string>(_stringLocalizer[SharedResourcesKeys.Deleted]);
		}

		public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
		{
			// Get User
			var User = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

			// If not exist => Not found
			if (User == null) return NotFound<string>();

			// if exist => change password
			var result = await _userManager.ChangePasswordAsync(User, request.CurrentPassword!, request.NewPassword!);
			// Not Success
			if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault()!.Description);

			return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
		}
		#endregion
	}
}
