using SchoolProject.Data.Entities;

namespace SchoolProject.Service.Abstracts
{
	public interface IDepartmentService
	{
		public Task<List<Department>> GetDepartmentList();
		public Task<Department> GetDepartmentById(int id);
		public Task<Department> GetDepartmentOnlyById(int id);
		public Task<Department> GetDepartment(int id);
		public Task<string> AddAsync(Department department);
		public Task<string> EditAsync(Department department);
		public Task<string> DeleteAsync(Department department);
		public Task<bool> IsNameExist(string name);
		public Task<bool> IsNameExcludeSelf(string name, int id);
		public Task<bool> IsManagerExcludeSelf(int id, int InstructorManager);
		public Task<bool> IsDepartmentIdExist(int? departmentId);
	}
}
