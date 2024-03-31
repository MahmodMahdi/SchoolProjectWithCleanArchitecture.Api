using System.Security.Claims;

namespace SchoolProject.Data.Helpers
{
	public static class ClaimsStore
	{
		public static List<Claim> claims = new()
		{
			new Claim ("Create Instructor","false"),
			new Claim ("Update Instructor","false"),
			new Claim ("Delete Instructor","false"),
			new Claim ("Create Department","false"),
			new Claim ("Update Department","false"),
			new Claim ("Delete Department","false"),
		};
	}
}
