using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Responses;

namespace SchoolProject.Service.Abstracts
{
	public interface IAuthorizationService
	{
		// Roles
		public Task<List<Role>> GetRolesList();
		public Task<Role> GetRoleById(int id);
		public Task<string> AddRoleAsync(string roleName);
		public Task<string> UpdateRoleAsync(UpdateRoleRequest request);
		public Task<string> DeleteRoleAsync(int roleId);
		public Task<bool> IsRoleExistAsync(string roleName);

		// UserRoles
		public Task<ManageUserRoleResponse> ManageUserRoles(User user);
		public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);

		//Claims
		public Task<ManageUserClaimsResponse> ManageUserClaims(User user);
		public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);

	}
}
