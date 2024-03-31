using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
	public class SubjectService : ISubjectService
	{
		#region Fields
		private readonly ISubjectRepository _subjectRepository;
		#endregion

		#region Constructor
		public SubjectService(ISubjectRepository subjectRepository)
		{
			_subjectRepository = subjectRepository;
		}
		#endregion

		#region Handle Functions
		public async Task<List<Subject>> GetSubjectListAsync()
		{
			var subject = await _subjectRepository.GetSubjectsAsync();
			return subject;
		}
		public async Task<Subject> GetSubjectOnlyById(int id)
		{
			var subject = await _subjectRepository.GetTableNoTracking()
										  .Where(x => x.SubjectID == id)
										  .FirstOrDefaultAsync();
			return subject!;
		}
		public async Task<Subject> GetSubjectById(int id)
		{
			var subject = await _subjectRepository.GetTableNoTracking()
										  .Where(x => x.SubjectID == id)
										  .Include(x => x.InstructorSubjects)
										  .ThenInclude(x => x.instructor)
										  .Include(x => x.DepartmentsSubjects)
										  .ThenInclude(x => x.Department)
										  .Include(x => x.StudentsSubjects)
										  .ThenInclude(x => x.Student)
										  .FirstOrDefaultAsync();
			return subject!;
		}
		public async Task<string> AddAsync(Subject subject)
		{
			// check if name is exist or not
			var Subject = _subjectRepository.GetTableNoTracking()
											.Where(x => x.SubjectNameEn!
											.Equals(subject.SubjectNameEn))
											.FirstOrDefault();
			if (Subject != null) { return "Exist"; }
			// Add Department
			await _subjectRepository.AddAsync(subject!);
			return "Success";
		}
		public async Task<string> EditAsync(Subject subject)
		{
			await _subjectRepository.UpdateAsync(subject);
			return "Success";
		}
		public async Task<string> DeleteAsync(Subject subject)
		{
			var transaction = await _subjectRepository.BeginTransactionAsync();
			try
			{
				await _subjectRepository.DeleteAsync(subject);
				await transaction.CommitAsync();
				return "Success";
			}
			catch
			{
				await transaction.RollbackAsync();
				return "Failed";
			}
		}
		public async Task<bool> IsNameExist(string name)
		{
			var subject = await _subjectRepository.GetTableNoTracking()
												  .Where(x => x.SubjectNameAr!.Equals(name) || x.SubjectNameEn!.Equals(name))
												  .FirstOrDefaultAsync();
			if (subject == null) { return false; }
			else return true;
		}
		public async Task<bool> IsNameExcludeSelf(string name, int id)
		{
			var subject = await _subjectRepository.GetTableNoTracking()
										.Where(x => x.SubjectNameEn!.Equals(name) && !x.SubjectID.Equals(id)
												 || x.SubjectNameAr!.Equals(name) && !x.SubjectID.Equals(id))
										.FirstOrDefaultAsync();
			if (subject == null) { return false; }
			else return true;
		}
		#endregion
	}
}
