using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
	public interface ISubjectService
	{
		public Task<List<Subject>> GetSubjectListAsync();
		public Task<Subject> GetSubjectOnlyById(int id);
		public Task<Subject> GetSubjectById(int id);
		public Task<string> AddAsync(Subject subject);
		public Task<string> EditAsync(Subject subject);
		public Task<string> DeleteAsync(Subject subject);
		public Task<bool> IsNameExist(string name);
		public Task<bool> IsNameExcludeSelf(string name, int id);
	}
}
