using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Interfaces
{
	public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
	{
		public Task<List<Department>> GetDepartmentsAsync();
	}
}
