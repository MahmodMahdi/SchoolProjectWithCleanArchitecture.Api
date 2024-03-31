using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
	public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
	{
		#region fields
		private readonly DbSet<Student> _students;
		#endregion
		#region Constructors
		public StudentRepository(ApplicationDBContext _context) : base(_context)
		{
			_students = _context.Set<Student>();
		}
		#endregion
		#region Handle Function
		public async Task<List<Student>> GetStudentsAsync()
		{
			var student = await _dbContext.students.Include(x => x.Department).ToListAsync();
			return student;
		}

		#endregion
	}
}
