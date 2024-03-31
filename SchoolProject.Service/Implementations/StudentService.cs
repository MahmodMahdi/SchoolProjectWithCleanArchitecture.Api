using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations;

public class StudentService : IStudentService
{
	#region Fields
	private readonly IStudentRepository _studentRepository;
	#endregion
	#region Constructors
	public StudentService(IStudentRepository studentRepository)
	{
		this._studentRepository = studentRepository;
	}
	#endregion
	#region Handle Functions
	public async Task<List<Student>> GetStudentsListAsync()
	{
		return await _studentRepository.GetStudentsAsync();
	}
	public async Task<Student> GetStudentByIdWithIncludeAsync(int id)
	{
		var student = (await _studentRepository.GetTableNoTracking()
										  .Where(x => x.Id == id)
										  .Include(x => x.Department)
										  .FirstOrDefaultAsync())!;
		return student;
	}
	public async Task<Student> GetByIdAsync(int id)
	{
		var student = await _studentRepository.GetByIdAsync(id);
		return student;
	}
	public async Task<string> AddAsync(Student student)
	{
		// check if name is exist or not
		var Student = _studentRepository.GetTableNoTracking()
										.Where(x => x.NameAr!
										.Equals(student.NameAr) && x.Phone!
										.Equals(student.Phone) || x.Phone!.Equals(student.Phone))
										.FirstOrDefault();
		if (Student != null) { return "Exist"; }
		// Add Student
		await _studentRepository.AddAsync(student);
		return "Success";
	}
	public async Task<string> EditAsync(Student student)
	{
		await _studentRepository.UpdateAsync(student);
		return "Success";
	}
	public async Task<string> DeleteAsync(Student student)
	{
		var transaction = await _studentRepository.BeginTransactionAsync();
		try
		{
			await _studentRepository.DeleteAsync(student);
			await transaction.CommitAsync();
			return "Success";
		}
		catch
		{
			await transaction.RollbackAsync();
			return "Failed";
		}
	}
	public async Task<bool> IsPhoneExist(string phone)
	{
		var student = await _studentRepository.GetTableNoTracking()
											  .Where(x => x.Phone!.Equals(phone))
											  .FirstOrDefaultAsync();
		if (student == null) { return false; }
		else return true;
	}
	public async Task<bool> IsPhoneExcludeSelf(string phone, int id)
	{
		var student = await _studentRepository.GetTableNoTracking()
											  .Where(x => x.Phone!.Equals(phone) && !x.Id.Equals(id))
											  .FirstOrDefaultAsync();
		if (student == null) { return false; }
		else return true;
	}
	//public async Task<bool> IsNameExist(string name)
	//{
	//	var Student = await _studentRepository.GetTableNoTracking()
	//									.Where(x => x.NameEn!
	//									.Equals(name) || x.NameAr!.Equals(name))
	//									.FirstOrDefaultAsync();
	//	if (Student == null) { return false; }
	//	else return true;
	//}

	//public async Task<bool> IsNameExcludeSelf(string name, int id)
	//{
	//	var Student = await _studentRepository.GetTableNoTracking()
	//									.Where(x => x.NameEn!
	//									.Equals(name) && !x.Id.Equals(id))
	//									.FirstOrDefaultAsync();
	//	if (Student == null) { return false; }
	//	else return true;
	//}
	public IQueryable<Student> GetStudentsQuerable()
	{
		return _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
	}
	public IQueryable<Student> GetStudentsQuerableByDepartmentId(int departmentId)
	{
		return _studentRepository.GetTableNoTracking().Where(x => x.DepartmentID.Equals(departmentId)).AsQueryable();
	}
	public IQueryable<Student> FilterStudentPaginatedQueryable(StudentOrderingEnum orderingEnum, string search)
	{
		var queryable = _studentRepository.GetTableNoTracking().Include(x => x.Department).AsQueryable();
		if (search != null)
		{
			queryable = queryable.Where(x => x.NameEn!.Contains(search) || x.Address!.Contains(search));
		}
		switch (orderingEnum)
		{
			case StudentOrderingEnum.Id:
				queryable = queryable.OrderBy(x => x.Id);
				break;
			case StudentOrderingEnum.Name:
				queryable = queryable.OrderBy(x => x.NameEn);
				break;
			case StudentOrderingEnum.Address:
				queryable = queryable.OrderBy(x => x.Address);
				break;
			case StudentOrderingEnum.Age:
				queryable = queryable.OrderBy(x => x.Age);
				break;
			case StudentOrderingEnum.DepartmentName:
				queryable = queryable.OrderBy(x => x.Department!.DepartmentNameEn);
				break;
		}
		return queryable;
	}
	#endregion
}
