using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authorization.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Authorization.Queries.Handlers
{
	public class ClaimsQueryHandler : ResponseHandler,
									IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResponse>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly IAuthorizationService _authorizationService;
		//private readonly IMapper _mapper;

		#endregion
		#region Constructor
		public ClaimsQueryHandler(
								  IStringLocalizer<SharedResources> stringLocalizer,
								  UserManager<User> userManager,
									 IAuthorizationService authorizationService
		// IMapper mapper
		) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_userManager = userManager;
			_authorizationService = authorizationService;
			//_mapper = mapper;
		}


		#endregion
		#region Handle Functions

		public async Task<Response<ManageUserClaimsResponse>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
		{
			// User
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());
			if (user == null) return NotFound<ManageUserClaimsResponse>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
			var result = await _authorizationService.ManageUserClaims(user);
			return Success(result);
		}

		#endregion
	}
}
