using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Core.Middleware;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.Seeding;
using SchoolProject.Service;
using System.Globalization;

namespace SchoolProject.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			#region Context
			//Connection to SQL Server 
			var DB = builder.Configuration.GetConnectionString("DB");
			builder.Services.AddDbContext<ApplicationDBContext>(options =>
			{
				options.UseSqlServer(DB);
			});
			#endregion

			#region Identity
			//builder.Services.AddIdentity<User, IdentityRole>(
			//options =>
			//{
			//    options.Password.RequireUppercase = false;
			//    options.Password.RequireNonAlphanumeric = false;
			//    options.Password.RequiredLength = 8;
			//    options.Password.RequireDigit = false;
			//})
			//    .AddEntityFrameworkStores<ApplicationDBContext>();
			// builder.Services.AddRegistration(builder.Configuration);
			#endregion



			#region Dependency Injections
			builder.Services.AddInfrastructureDependencies()
				   .AddRegistration(builder.Configuration)
				   .AddServiceDependencies()
				   .AddCoreDependencies();
			#endregion

			#region Localization
			builder.Services.AddControllersWithViews();
			builder.Services.AddLocalization(opt =>
			{
				opt.ResourcesPath = "";
			});

			builder.Services.Configure<RequestLocalizationOptions>(options =>
			{
				List<CultureInfo> supportedCultures = new List<CultureInfo>
				{
						 new CultureInfo("en-US"),
						 new CultureInfo("de-DE"),
						 new CultureInfo("fr-FR"),
						 new CultureInfo("ar-EG")
				};

				options.DefaultRequestCulture = new RequestCulture("ar-EG");
				options.SupportedCultures = supportedCultures;
				options.SupportedUICultures = supportedCultures;
			});


			#endregion

			#region Cors
			var MyCors = "MyCors";
			builder.Services.AddCors(option =>
			option.AddPolicy(name: MyCors, policy =>
			{
				// when i need to specify
				//policy.WithOrigins("http://example.com");
				policy.AllowAnyOrigin();
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
			}));
			#endregion

			builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
			builder.Services.AddTransient<IUrlHelper>(x =>
			{
				var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
				var factory = x.GetRequiredService<IUrlHelperFactory>();
				return factory.GetUrlHelper(actionContext!);
			});

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

				RoleSeeding.SeedAsync(roleManager);
				UserSeeding.SeedAsync(userManager);
			}

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			#region Localization Middleware
			var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>()!;
			app.UseRequestLocalization(options.Value);
			#endregion

			app.UseHttpsRedirection();
			app.UseMiddleware<ErrorHandlerMiddleware>();
			// Add Cors
			app.UseCors(MyCors);

			app.UseStaticFiles();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}
