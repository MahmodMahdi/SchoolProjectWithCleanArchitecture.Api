using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Interfaces
{
	public interface IInstructorRepository : IGenericRepositoryAsync<Instructor>
	{
		public Task<List<Instructor>> GetInstructorsAsync();
	}
}
