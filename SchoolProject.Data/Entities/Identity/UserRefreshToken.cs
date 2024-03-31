using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities.Identity
{
	public class UserRefreshToken
	{
		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public string? Token { get; set; }
		public string? RefreshToken { get; set; }
		public string? JwtId { get; set; }
		public bool IsUsed { get; set; }
		public bool IsRevoked { get; set; }
		public DateTime AddedTime { get; set; }
		public DateTime ExpireDate { get; set; }
		[ForeignKey(nameof(UserId))]
		[InverseProperty("UserRefreshTokens")]
		public virtual User? user { get; set; }
	}
}
