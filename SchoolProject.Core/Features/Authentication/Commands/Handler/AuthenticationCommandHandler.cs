using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;
using SchoolProject.Service.Abstracts;
namespace SchoolProject.Core.Features.Authentication.Commands.Handler
{
	public class AuthenticationCommandHandler : ResponseHandler,
										IRequestHandler<SignInCommand, Response<JwtAuthenticationResponse>>,
										   IRequestHandler<RefreshTokenCommand, Response<JwtAuthenticationResponse>>,
										   IRequestHandler<SendResetPasswordCommand, Response<string>>,
										   IRequestHandler<ResetPasswordCommand, Response<string>>
	{
		#region Fields
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IAuthenticationService _authenticationService;
		#endregion
		#region Constructor
		public AuthenticationCommandHandler(
								  IStringLocalizer<SharedResources> stringLocalizer,
								  UserManager<User> userManager,
								  SignInManager<User> signInManager,
								  IAuthenticationService authenticationService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_userManager = userManager;
			_signInManager = signInManager;
			_authenticationService = authenticationService;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<JwtAuthenticationResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
		{
			// Check if user is exist or not 
			var user = await _userManager.FindByEmailAsync(request.Email!);

			// return Email not found
			if (user == null) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.EmailIsNotExist]);
			// try to login
			var Result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);
			////if failed return password is wrong
			if (!Result.Succeeded) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.PasswordNotCorrect]);
			// confirm email 
			if (!user.EmailConfirmed) return BadRequest<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.EmailNotConfirmed]);
			//// Generate token
			var token = await _authenticationService.GetJWTToken(user);
			// return token
			return Success(token);
		}

		public async Task<Response<JwtAuthenticationResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
		{
			var jwtToken = _authenticationService.ReadJwtToken(request.Token!);
			var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.Token!, request.RefreshToken!);
			switch (userIdAndExpireDate)
			{
				case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.AlgorithmIsWrong]);
				case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.TokenIsNotExpired]);
				case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsNotFound]);
				case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthenticationResponse>(_stringLocalizer[SharedResourcesKeys.RefreshTokenIsExpired]);
			}
			var (userId, expireDate) = userIdAndExpireDate;
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null) return NotFound<JwtAuthenticationResponse>();
			var result = await _authenticationService.GetRefreshToken(user, jwtToken, expireDate, request.RefreshToken!);
			return Success(result);
		}

		public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.SendResetPasswordCode(request.Email);
			switch (result)
			{
				case ("UserNotFound"): return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
				case ("ErrorInUpdateUser"): return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
				case ("Failed"): return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
				case ("Success"): return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
				default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
			}
		}

		public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.ResetPassword(request.Email, request.Password);
			switch (result)
			{
				case ("UserNotFound"): return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
				case ("Failed"): return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
				case ("Success"): return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
				default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.SomeThingGoesWrong]);
			}
		}
		#endregion
	}

}
