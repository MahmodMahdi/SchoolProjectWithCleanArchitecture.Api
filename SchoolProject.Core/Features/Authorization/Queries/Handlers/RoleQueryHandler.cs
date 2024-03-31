using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Dtos;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
	public class RoleQueryHandler : ResponseHandler,
									IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResponse>>>,
									IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResponse>>,
									IRequestHandler<ManageUserRoleQuery, Response<ManageUserRoleResponse>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly IAuthorizationService _authorizationService;
		private readonly IMapper _mapper;

		#endregion
		#region Constructor
		public RoleQueryHandler(
								  IStringLocalizer<SharedResources> stringLocalizer,
								  UserManager<User> userManager,
								  IAuthorizationService authorizationService,
								   IMapper mapper) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_userManager = userManager;
			_authorizationService = authorizationService;
			_mapper = mapper;
		}




		#endregion
		#region Handle Functions
		public async Task<Response<List<GetRolesListResponse>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
		{
			var roles = await _authorizationService.GetRolesList();
			var result = _mapper.Map<List<GetRolesListResponse>>(roles);
			return Success(result);
		}

		public async Task<Response<GetRoleByIdResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
		{
			var role = await _authorizationService.GetRoleById(request.Id);
			if (role == null) return NotFound<GetRoleByIdResponse>(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
			var result = _mapper.Map<GetRoleByIdResponse>(role);
			return Success(result);
		}

		public async Task<Response<ManageUserRoleResponse>> Handle(ManageUserRoleQuery request, CancellationToken cancellationToken)
		{
			// User
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null) return NotFound<ManageUserRoleResponse>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
			var result = await _authorizationService.ManageUserRoles(user);
			return Success(result);
		}
		#endregion
	}
}
