using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolProject.Service.Abstracts
{
	public interface IAuthenticationService
	{
		public Task<JwtAuthenticationResponse> GetJWTToken(User user);
		public JwtSecurityToken ReadJwtToken(string Token);
		public Task<JwtAuthenticationResponse> GetRefreshToken(User user, JwtSecurityToken JwtToken, DateTime? ExpireDate, string RefreshToken);
		public Task<string> ValidateToken(string Token);
		public Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string Token, string refreshToken);

		public Task<string> ConfirmEmail(int? UserId, string Code);
		public Task<string> SendResetPasswordCode(string Email);
		public Task<string> ConfirmResetPassword(string Code, string Email);
		public Task<string> ResetPassword(string Email, string Password);


	}
}
