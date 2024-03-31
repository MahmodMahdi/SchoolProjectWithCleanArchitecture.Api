using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Responses;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace SchoolProject.Service.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		#region Fields
		private readonly JwtSettings _jwtSettings;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly UserManager<User> _userManager;
		private readonly IEmailService _emailService;
		private readonly ApplicationDBContext _context;
		private readonly IEncryptionProvider _encryptionProvider;
		#endregion

		#region Constructor
		public AuthenticationService(JwtSettings jwtSettings,
			IRefreshTokenRepository refreshTokenRepository,
			UserManager<User> userManager,
			IEmailService emailService,
			ApplicationDBContext context)
		{
			_jwtSettings = jwtSettings;
			_refreshTokenRepository = refreshTokenRepository;
			_userManager = userManager;
			_emailService = emailService;
			_context = context;
			_encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
		}
		#endregion

		#region Handle Functions
		public async Task<JwtAuthenticationResponse> GetJWTToken(User user)
		{
			#region Token
			var (jwtToken, Token) = await GenerateJwtToken(user);
			#endregion
			#region RefreshToken
			var refreshToken = GetRefreshToken(user.Email!);

			// save items of refresh token
			var userRefreshToken = new UserRefreshToken
			{
				UserId = user.Id,
				Token = Token,
				RefreshToken = refreshToken.refreshTokenString,
				JwtId = jwtToken.Id,
				IsUsed = true,
				IsRevoked = false,
				AddedTime = DateTime.Now,
				ExpireDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
			};
			await _refreshTokenRepository.AddAsync(userRefreshToken);

			var result = new JwtAuthenticationResponse();
			result.RefreshToken = refreshToken;
			result.Token = Token;
			return result;
			#endregion
		}

		// here to Generate Token(first)
		private async Task<(JwtSecurityToken, string)> GenerateJwtToken(User user)
		{
			var claims = await GetClaims(user);

			var jwtToken = new JwtSecurityToken(
				_jwtSettings.Issuer,
				_jwtSettings.Audience,
				claims,
				expires: DateTime.Now.AddDays(_jwtSettings.TokenExpireDate),
				signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret!)),
				SecurityAlgorithms.HmacSha256Signature));
			var Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			return (jwtToken, Token);
		}
		public async Task<List<Claim>> GetClaims(User user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			var claims = new List<Claim>()
			 {
				new Claim(nameof(UserClaimsModel.Id), user.Id!.ToString()),
				new Claim(nameof(ClaimTypes.NameIdentifier),user.UserName!),
				new Claim(nameof(UserClaimsModel.PhoneNumber), user.PhoneNumber!),
				new Claim(nameof(ClaimTypes.Email), user.Email!),
			};
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			var userClaims = await _userManager.GetClaimsAsync(user);
			claims.AddRange(userClaims);
			return claims;
		}
		private RefreshToken GetRefreshToken(string Email)
		{
			// Refresh Token
			var refreshToken = new RefreshToken
			{
				ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
				Email = Email,
				refreshTokenString = GenerateRefreshToken()
			};
			return refreshToken;
		}

		public async Task<JwtAuthenticationResponse> GetRefreshToken(User user, JwtSecurityToken JwtToken, DateTime? ExpireDate, string RefreshToken)
		{
			var (jwtSecurityToken, newToken) = await GenerateJwtToken(user!);

			var Result = new JwtAuthenticationResponse();
			Result.Token = newToken;
			var RefreshTokenResult = new RefreshToken();
			RefreshTokenResult.Email = JwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Email))!.Value;
			RefreshTokenResult.refreshTokenString = RefreshToken;
			RefreshTokenResult.ExpireAt = (DateTime)ExpireDate!;
			Result.RefreshToken = RefreshTokenResult;
			return Result;
		}

		public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string Token, string refreshToken)
		{
			if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature)) { return ("AlgorithmIsWrong", null); }
			if (jwtToken.ValidTo > DateTime.UtcNow) { return ("TokenIsNotExpired", null); }
			// GetUser
			var Id = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimsModel.Id))!.Value;
			var userRT = await _refreshTokenRepository.GetTableNoTracking()
				.FirstOrDefaultAsync(x => x.Token == Token && x.RefreshToken == refreshToken && x.UserId == int.Parse(Id));
			if (userRT == null) return ("RefreshTokenIsNotFound", null);
			// Validations Token , RefreshToken
			if (userRT!.ExpireDate < DateTime.UtcNow)
			{
				userRT.IsRevoked = true;
				userRT.IsUsed = false;
				await _refreshTokenRepository.UpdateAsync(userRT);
				return ("RefreshTokenIsExpired", null);
			}

			var expireDate = userRT.ExpireDate;
			return (Id, expireDate);
		}

		public JwtSecurityToken ReadJwtToken(string Token)
		{
			if (string.IsNullOrEmpty(Token)) throw new ArgumentNullException(nameof(Token));
			var handler = new JwtSecurityTokenHandler();
			var result = handler.ReadJwtToken(Token);
			return result;
		}

		// check of validate token
		public async Task<string> ValidateToken(string Token)
		{
			var handler = new JwtSecurityTokenHandler();
			var parameters = new TokenValidationParameters
			{
				ValidateIssuer = _jwtSettings.ValidateIssuer,
				ValidIssuers = new[] { _jwtSettings.Issuer },
				ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret!)),
				ValidAudience = _jwtSettings.Audience,
				ValidateAudience = _jwtSettings.ValidateAudience,
				ValidateLifetime = _jwtSettings.ValidateLifeTime,
			};
			try
			{
				// Validation
				var validator = handler.ValidateToken(Token, parameters, out SecurityToken validatedToken);
				if (validator == null) { throw new SecurityTokenException("Invalid Token"); }
				return "NotExpired";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
		public async Task<string> ConfirmEmail(int? UserId, string Code)
		{
			if (UserId == null || Code == null) { return "ErrorInConfirmEmail"; }
			var user = await _userManager.FindByIdAsync(UserId.ToString()!);
			var ConfirmEmail = await _userManager.ConfirmEmailAsync(user!, Code);
			if (!ConfirmEmail.Succeeded) { return "ErrorInConfirmEmail"; }
			return "Success";
		}

		public async Task<string> SendResetPasswordCode(string Email)
		{
			var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// get user 
				var user = await _userManager.FindByEmailAsync(Email);
				// if not exist => not found
				if (user == null) { return "UserNotFound"; }
				// generate random number
				Random generator = new Random();
				string random = generator.Next(0, 1000000).ToString("D6");
				// update user in db code
				user.Code = random;
				var Updated = await _userManager.UpdateAsync(user);
				if (!Updated.Succeeded) { return "ErrorInUpdateUser"; }
				// Send code to email 
				var message = "Code to Reset Password : " + user.Code;
				await _emailService.SendEmailAsync(user.Email!, message, "Reset Password");
				await transaction.CommitAsync();
				return "Success";
			}

			catch (Exception)
			{
				await transaction.RollbackAsync();
				return "Failed";
			}
		}
		public async Task<string> ConfirmResetPassword(string Code, string Email)
		{
			// get code from db
			var user = await _userManager.FindByEmailAsync(Email);
			if (user == null) return "UserNotFound";
			// decrypt code from db
			var UserCode = user.Code;
			if (UserCode == Code) return "Success";
			return "Failed";


		}

		public async Task<string> ResetPassword(string Email, string Password)
		{
			var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// get code from db
				var user = await _userManager.FindByEmailAsync(Email);
				if (user == null) return "UserNotFound";
				await _userManager.RemovePasswordAsync(user);
				await _userManager.AddPasswordAsync(user, Password);
				await transaction.CommitAsync();
				return "Success";
			}

			catch (Exception)
			{
				await transaction.RollbackAsync();
				return "Failed";
			}
		}
		// here to create a random Number for RT
		private string GenerateRefreshToken()
		{
			var RandomNumber = new byte[32];
			var randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(RandomNumber);
			return Convert.ToBase64String(RandomNumber);
		}
		#endregion
	}
}
