using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Interfaces
{
	public interface ISubjectRepository : IGenericRepositoryAsync<Subject>
	{
		public Task<List<Subject>> GetSubjectsAsync();
	}
}
