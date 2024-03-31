using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Service.Abstracts;
using SchoolProject.Service.Implementations;


namespace SchoolProject.Service
{
	public static class ModuleServiceDependency
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			services.AddTransient<IStudentService, StudentService>();
			services.AddTransient<IDepartmentService, DepartmentService>();
			services.AddTransient<IInstructorService, InstructorService>();
			services.AddTransient<ISubjectService, SubjectService>();
			services.AddTransient<IAuthenticationService, AuthenticationService>();
			services.AddTransient<IAuthorizationService, AuthorizationService>();
			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<IApplicationUserService, ApplicationUserService>();
			services.AddTransient<IFileService, FileService>();
			return services;
		}
	}
}
