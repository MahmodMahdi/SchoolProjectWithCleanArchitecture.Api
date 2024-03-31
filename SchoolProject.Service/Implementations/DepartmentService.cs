using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
	public class DepartmentService : IDepartmentService
	{
		#region Fields
		private readonly IDepartmentRepository _departmentRepository;
		#endregion

		#region Constructor
		public DepartmentService(IDepartmentRepository departmentRepository)
		{
			_departmentRepository = departmentRepository;
		}
		#endregion

		#region Handle Functions
		public async Task<List<Department>> GetDepartmentList()
		{
			var department = await _departmentRepository.GetDepartmentsAsync();
			return department;
		}
		public async Task<Department> GetDepartmentOnlyById(int id)
		{
			var Department = (await _departmentRepository.GetTableNoTracking()
										   .Where(x => x.DepartmentID == id)
										   .Include(x => x.Instructor)
										   .FirstOrDefaultAsync())!;
			return Department;
		}
		public async Task<Department> GetDepartment(int id)
		{
			var Department = (await _departmentRepository.GetTableNoTracking()
										   .Where(x => x.DepartmentID == id)
										   .FirstOrDefaultAsync())!;
			return Department;
		}
		public async Task<Department> GetDepartmentById(int id)
		{
			var department = (await _departmentRepository.GetTableNoTracking()
										   .Where(x => x.DepartmentID == id)
										   .Include(x => x.DepartmentSubjects).ThenInclude(x => x.Subject)
										   .Include(x => x.Students)
										   .Include(x => x.Instructors)
										   .Include(x => x.Instructor)
										   .FirstOrDefaultAsync())!;
			return department;
		}
		public async Task<string> AddAsync(Department department)
		{
			// check if name is exist or not
			var Department = _departmentRepository.GetTableNoTracking()
											.Where(x => x.DepartmentNameAr!
											.Equals(department.DepartmentNameAr))
											.FirstOrDefault();
			if (Department != null) { return "Exist"; }
			// Add Department
			await _departmentRepository.AddAsync(department);
			return "Success";
		}
		public async Task<string> EditAsync(Department department)
		{
			await _departmentRepository.UpdateAsync(department);
			return "Success";
		}
		public async Task<string> DeleteAsync(Department department)
		{
			var transaction = await _departmentRepository.BeginTransactionAsync();
			try
			{
				await _departmentRepository.DeleteAsync(department);
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
			var student = await _departmentRepository.GetTableNoTracking()
												  .Where(x => x.DepartmentNameAr!.Equals(name) || x.DepartmentNameEn!.Equals(name))
												  .FirstOrDefaultAsync();
			if (student == null) { return false; }
			else return true;
		}
		public async Task<bool> IsNameExcludeSelf(string name, int id)
		{
			var Department = await _departmentRepository.GetTableNoTracking()
										.Where(x => x.DepartmentNameEn!.Equals(name) && !x.DepartmentID.Equals(id)
											|| x.DepartmentNameAr!.Equals(name) && !x.DepartmentID.Equals(id))
										.FirstOrDefaultAsync();
			if (Department == null) { return false; }
			else return true;
		}
		public async Task<bool> IsManagerExcludeSelf(int instructorManager, int id)
		{
			var Manager = await _departmentRepository.GetTableNoTracking()
										.Where(x => x.DepartmentManager.Equals(instructorManager) && !x.DepartmentID.Equals(id))
										.FirstOrDefaultAsync();
			if (Manager == null) { return false; }
			else return true;
		}
		public async Task<bool> IsDepartmentIdExist(int departmentId)
		{
			var DepartmentId = await _departmentRepository.GetTableNoTracking()
												  .AnyAsync(x => x.DepartmentID!.Equals(departmentId));
			return DepartmentId;
		}
		#endregion
	}
}
