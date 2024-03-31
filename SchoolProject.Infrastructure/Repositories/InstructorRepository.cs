using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
	public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
	{
		#region Fields
		private readonly DbSet<Instructor> _instructor;
		#endregion

		#region Constructor
		public InstructorRepository(ApplicationDBContext context) : base(context)
		{
			_instructor = context.Set<Instructor>();
		}
		#endregion

		#region Handle Functions
		public async Task<List<Instructor>> GetInstructorsAsync()
		{
			var instructors = await _dbContext.instructors.Include(x => x.InstructorSubjects).ThenInclude(x => x.Subject)
												 .Include(x => x.department)
												 .Include(x => x.departmentManager)
												 .Include(x => x.Supervisor)
												 .Include(x => x.Instructors)
												 .ToListAsync();
			return instructors;
		}
		#endregion
	}
}
