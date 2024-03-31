using Microsoft.AspNetCore.Http;
using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
	public interface IInstructorService
	{
		public Task<List<Instructor>> GetInstructorList();
		public Task<Instructor> GetInstructorById(int id);
		public Task<Instructor> GetInstructorOnlyById(int id);
		public Task<Instructor> GetInstructor(int id);
		public Task<string> AddAsync(Instructor Instructor, IFormFile file);
		public Task<string> EditAsync(Instructor Instructor, IFormFile file);
		public Task<string> DeleteAsync(Instructor Instructor);
		public Task<bool> IsPhoneExist(string phone);
		public Task<bool> IsPhoneExcludeSelf(string phone, int id);
		public Task<bool> IsSupervisorIdExist(int supervisorId);
		public Task<bool> IsDepartmentManagerIdExist(int DepartmentManager);
	}
}