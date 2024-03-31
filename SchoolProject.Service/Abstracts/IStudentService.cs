using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;

namespace SchoolProject.Service.Abstracts
{
	public interface IStudentService
	{
		public Task<List<Student>> GetStudentsListAsync();
		public Task<Student> GetStudentByIdWithIncludeAsync(int id);
		public Task<Student> GetByIdAsync(int id);
		public Task<string> AddAsync(Student student);
		public Task<string> EditAsync(Student student);
		public Task<string> DeleteAsync(Student student);
		//public Task<bool> IsNameExist(string name);
		//public Task<bool> IsNameExcludeSelf(string name, int id);
		public Task<bool> IsPhoneExist(string phone);
		public Task<bool> IsPhoneExcludeSelf(string phone, int id);
		public IQueryable<Student> GetStudentsQuerable();
		public IQueryable<Student> GetStudentsQuerableByDepartmentId(int departmentId);
		public IQueryable<Student> FilterStudentPaginatedQueryable(StudentOrderingEnum orderingEnum, string search);
	}
}
