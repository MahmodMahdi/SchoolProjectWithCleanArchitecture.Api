using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
	public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
	{
		#region Fields
		private readonly DbSet<Subject> _subject;
		#endregion

		#region Constructor
		public SubjectRepository(ApplicationDBContext context) : base(context)
		{
			_subject = context.Set<Subject>();
		}
		#endregion

		#region Handle Functions
		public async Task<List<Subject>> GetSubjectsAsync()
		{

			var subject = await _dbContext.subjects.ToListAsync();
			return subject;
		}
		#endregion
	}
}
