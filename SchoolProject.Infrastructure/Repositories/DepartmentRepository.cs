using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
	public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
	{
		#region Fields
		private readonly DbSet<Department> _department;
		#endregion

		#region Constructor
		public DepartmentRepository(ApplicationDBContext context) : base(context)
		{
			_department = context.Set<Department>();
		}
		#endregion

		#region Handle Functions
		public async Task<List<Department>> GetDepartmentsAsync()
		{
			var departments = await _dbContext.departments.Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
												 .Include(x => x.Students)
												 .Include(x => x.Instructors)
												 .Include(x => x.Instructor).ToListAsync();
			return departments;
		}
		#endregion
	}
}
