using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities.Identity
{
	public class User : IdentityUser<int>
	{
		public User()
		{
			UserRefreshTokens = new HashSet<UserRefreshToken>();
		}
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public override string? UserName { get; set; }
		public string? Address { get; set; }
		[EncryptColumn]
		public string? Code { get; set; }
		[InverseProperty("user")]
		public virtual ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }
	}
}
