using Microsoft.Extensions.DependencyInjection;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Infrastructure.Repositories;
namespace SchoolProject.Infrastructure
{
	public static class ModuleInfrastructureDependency
	{
		public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
		{
			services.AddTransient<IStudentRepository, StudentRepository>();
			services.AddTransient<IDepartmentRepository, DepartmentRepository>();
			services.AddTransient<IInstructorRepository, InstructorRepository>();
			services.AddTransient<ISubjectRepository, SubjectRepository>();
			services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
			return services;
		}

	}
}
