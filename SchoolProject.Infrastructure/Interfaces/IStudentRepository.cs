using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Interfaces;
public interface IStudentRepository : IGenericRepositoryAsync<Student>
{
	public Task<List<Student>> GetStudentsAsync();
}
