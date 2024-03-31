using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeding
{
	public static class RoleSeeding
	{
		public static void SeedAsync(RoleManager<Role> roleManager)
		{
			var roles = roleManager.Roles.CountAsync().GetAwaiter().GetResult();
			if (roles <= 0)
			{
				roleManager.CreateAsync(new Role()
				{
					Name = "Admin"
				}).GetAwaiter().GetResult();
				roleManager.CreateAsync(new Role()
				{
					Name = "User"
				}).GetAwaiter().GetResult();
			}
		}
	}
}
