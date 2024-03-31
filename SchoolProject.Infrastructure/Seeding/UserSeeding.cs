using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Infrastructure.Seeding
{
	public static class UserSeeding
	{
		public static void SeedAsync(UserManager<User> userManager)
		{
			var Users = userManager.Users.CountAsync().GetAwaiter().GetResult();
			if (Users <= 0)
			{

				var FirstUser = new User()
				{
					UserName = "Mahmoud@gmail.com",
					Email = "Mahmoud@gmail.com",
					FirstName = "Mahmoud",
					LastName = "Amin",
					PhoneNumber = "01212345654",
					Address = "Tanta",
					EmailConfirmed = true,
					PhoneNumberConfirmed = true,
				};
				userManager.CreateAsync(FirstUser, "Mahmoud123").GetAwaiter().GetResult();
				userManager.AddToRoleAsync(FirstUser, "Admin").GetAwaiter().GetResult();
			}
		}
	}
}
