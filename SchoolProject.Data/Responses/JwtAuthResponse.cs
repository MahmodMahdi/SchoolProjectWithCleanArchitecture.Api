namespace SchoolProject.Data.Responses
{
	public class JwtAuthenticationResponse
	{
		public string? Token { get; set; }
		public RefreshToken? RefreshToken { get; set; }
	}
	public class RefreshToken
	{
		public string? Email { get; set; }
		public string? refreshTokenString { get; set; }
		public DateTime ExpireAt { get; set; }
	}
}
