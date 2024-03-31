using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Requests;
using SchoolProject.Data.Responses;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Service.Abstracts;
using System.Data;
using System.Security.Claims;

namespace SchoolProject.Service.Implementations
{
	public class AuthorizationService : IAuthorizationService
	{
		#region Fields
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;
		private readonly ApplicationDBContext _dbContext;
		#endregion

		#region Constructor
		public AuthorizationService(RoleManager<Role> roleManager,
			UserManager<User> userManager,
			ApplicationDBContext dbContext)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_dbContext = dbContext;
		}
		#endregion

		#region Handle Functions

		#region Roles
		public async Task<List<Role>> GetRolesList()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			return roles;
		}
		public async Task<Role> GetRoleById(int id)
		{
			var role = await _roleManager.FindByIdAsync(id.ToString());
			return role!;
		}
		public async Task<string> AddRoleAsync(string roleName)
		{
			var IdentityRole = new Role();
			IdentityRole.Name = roleName;
			var result = await _roleManager.CreateAsync(IdentityRole);
			if (result.Succeeded) return "Success";
			return "Failed";
		}
		public async Task<string> UpdateRoleAsync(UpdateRoleRequest request)
		{
			// check of existing
			var role = await _roleManager.FindByIdAsync(request.Id.ToString());
			// if not exist (Not found)
			if (role == null) return "NotFound";
			role.Name = request.RoleName;
			// Update
			var result = await _roleManager.UpdateAsync(role);
			//Success
			if (result.Succeeded) return "Success";
			//not success
			var errors = string.Join("-", result.Errors);
			return errors;
		}
		public async Task<string> DeleteRoleAsync(int RoleId)
		{
			// check on existing of role
			var role = await _roleManager.FindByIdAsync(RoleId.ToString());
			if (role == null) return "NotFound";
			// check if user has this role => return exp
			var user = await _userManager.GetUsersInRoleAsync(role.Name!);
			if (user != null && user!.Count() > 0) return "RoleIsUsed";
			// delete
			var result = await _roleManager.DeleteAsync(role);
			// success
			if (result.Succeeded) return "Success";
			var errors = string.Join("-", result.Errors);
			return errors;

		}
		public async Task<bool> IsRoleExistAsync(string roleName)
		{
			var role = await _roleManager.RoleExistsAsync(roleName);
			return role;
		}
		#endregion

		#region Manage UserRoles
		public async Task<ManageUserRoleResponse> ManageUserRoles(User user)
		{
			var result = new ManageUserRoleResponse();
			var rolesList = new List<UserRoles>();
			// Roles
			var Roles = await _roleManager.Roles.ToListAsync();

			// fill the ManageUserRoleResponse
			result.UserId = user.Id;

			foreach (var role in Roles)
			{
				var userRoleResult = new UserRoles();
				userRoleResult.RoleId = role.Id;
				userRoleResult.RoleName = role.Name!;
				// Check of that user has role => if has then true else false
				if (await _userManager.IsInRoleAsync(user, role.Name!)) { userRoleResult.HasRole = true; }
				else { userRoleResult.HasRole = false; }
				// add the filled list to list
				rolesList.Add(userRoleResult);
			}
			result.UserRoles = rolesList;
			// if UserRoles contain UserRoles
			return result;
		}
		public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
		{
			// use Transaction and RoleBack to avoid any conflict or errors that happen when remove or add
			var transaction = await _dbContext.Database.BeginTransactionAsync();
			try
			{
				// Get User 
				var user = await _userManager.FindByIdAsync(request.UserId.ToString());
				if (user == null) return "UserIsNull";

				// Get old UserRoles
				var userRoles = await _userManager.GetRolesAsync(user);

				// Delete old Roles
				var removed = await _userManager.RemoveFromRolesAsync(user, userRoles);
				if (!removed.Succeeded) { return "FailedToRemoveOldRoles"; }

				// Add Roles => has = true
				var OwnedRoles = request.UserRoles!.Where(x => x.HasRole == true).Select(x => x.RoleName);
				var result = await _userManager.AddToRolesAsync(user, OwnedRoles!);
				if (!result.Succeeded) { return "FailedToAddNewRoles"; }
				await transaction.CommitAsync();
				// return result
				return "Success";
			}
			catch
			{
				await transaction.RollbackAsync();
				return "FailedToUpdateUserRoles";
			}

		}
		#endregion

		#region UserClaims
		public async Task<ManageUserClaimsResponse> ManageUserClaims(User user)
		{
			var response = new ManageUserClaimsResponse();
			var ClaimsList = new List<UserClaims>();
			response.UserId = user.Id;
			// Get UserClaims
			var userClaims = await _userManager.GetClaimsAsync(user);
			// Check Existing of Claim for User => True
			foreach (var claim in ClaimsStore.claims)
			{
				var userClaimResult = new UserClaims();
				userClaimResult.Type = claim.Type;
				if (userClaims.Any(x => x.Type == claim.Type)) { userClaimResult.Value = true; }
				else { userClaimResult.Value = false; }
				ClaimsList.Add(userClaimResult);
			}
			response.UserClaims = ClaimsList;
			// return result
			return response;
		}

		public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
		{
			// use Transaction and RoleBack to avoid any conflict or errors that happen when remove or add
			var transaction = await _dbContext.Database.BeginTransactionAsync();
			try
			{
				// Get User 
				var user = await _userManager.FindByIdAsync(request.UserId.ToString());
				if (user == null) return "UserIsNull";

				// Get old UserClaims
				var userClaims = await _userManager.GetClaimsAsync(user);

				// Delete old Claims
				var removed = await _userManager.RemoveClaimsAsync(user, userClaims);
				if (!removed.Succeeded) { return "FailedToRemoveOldClaims"; }

				// Add Claims => has = true
				var OwnedClaims = request.UserClaims!.Where(x => x.Value == true).Select(x => new Claim(x.Type!, x.Value.ToString()));
				var result = await _userManager.AddClaimsAsync(user, OwnedClaims!);
				if (!result.Succeeded) { return "FailedToAddNewClaims"; }
				await transaction.CommitAsync();
				// return result
				return "Success";
			}
			catch
			{
				await transaction.RollbackAsync();
				return "FailedToUpdateUserClaims";
			}
		}
		#endregion

		#endregion
	}
}
