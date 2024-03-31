namespace SchoolProject.Data.Responses
{
	public class ManageUserRoleResponse
	{
		public int UserId { get; set; }
		public List<UserRoles>? UserRoles { get; set; }
	}
	public class UserRoles
	{
		public int RoleId { get; set; }
		public string? RoleName { get; set; }
		public bool HasRole { get; set; }
	}
}
