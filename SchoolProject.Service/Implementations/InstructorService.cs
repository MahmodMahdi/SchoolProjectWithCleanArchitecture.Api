using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Service.Implementations
{
	public class InstructorService : IInstructorService
	{
		#region Fields
		private readonly IInstructorRepository _instructorRepository;
		private readonly IFileService _fileService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IWebHostEnvironment _webHost;
		#endregion

		#region Constructor
		public InstructorService(IInstructorRepository instructorRepository
			, IFileService fileService
			, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHost)
		{
			_instructorRepository = instructorRepository;
			_fileService = fileService;
			_httpContextAccessor = httpContextAccessor;
			_webHost = webHost;
		}
		#endregion

		#region Handle Functions
		public async Task<List<Instructor>> GetInstructorList()
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var instructors = await _instructorRepository.GetInstructorsAsync();

			foreach (var instructor in instructors)
			{
				if (instructor.Image != null)
				{
					instructor.Image = baseUrl + instructor.Image;
				}
			}

			return instructors;
		}
		public async Task<Instructor> GetInstructorById(int id)
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var instructor = (await _instructorRepository.GetTableNoTracking()
										   .Where(x => x.InstructorId == id)
										   .Include(x => x.department)
										   .Include(x => x.departmentManager)
										   .Include(x => x.Supervisor)
										   .Include(x => x.InstructorSubjects).ThenInclude(x => x.Subject)
										   .Include(x => x.Instructors)
										   .FirstOrDefaultAsync())!;
			if (instructor.Image != null)
			{
				instructor.Image = baseUrl + instructor.Image;
			}
			return instructor;
		}
		public async Task<Instructor> GetInstructorOnlyById(int id)
		{
			var context = _httpContextAccessor.HttpContext!.Request;
			var baseUrl = context.Scheme + "://" + context.Host;
			var instructor = (await _instructorRepository.GetTableNoTracking()
										   .Where(x => x.InstructorId == id)
										   .Include(x => x.department)
										   .Include(x => x.departmentManager)
										   .Include(x => x.Supervisor)
										   .FirstOrDefaultAsync())!;
			if (instructor != null)
			{
				if (instructor.Image != null)
				{
					instructor.Image = baseUrl + instructor.Image;
				}
			}
			return instructor!;
		}
		public async Task<Instructor> GetInstructor(int id)
		{
			var instructor = (await _instructorRepository.GetTableNoTracking()
										   .Where(x => x.InstructorId == id)
										   .FirstOrDefaultAsync())!;
			return instructor;
		}
		public async Task<string> AddAsync(Instructor Instructor, IFormFile file)
		{

			var imageUrl = await _fileService.UploadImage("Instructor", file);
			Instructor.Image = imageUrl;
			switch (imageUrl)
			{
				case "NoImage": return "NoImage";
				case "FailedToUploadImage": return "FailedToUploadImage";
			}
			// check if name is exist or not
			var instructor = _instructorRepository.GetTableNoTracking()
											.Where(x => x.NameEn!
											.Equals(Instructor.NameEn))
											.FirstOrDefault();
			if (instructor != null) { return "Exist"; }
			// Add Department
			try
			{
				await _instructorRepository.AddAsync(Instructor!);
				return "Success";
			}
			catch (Exception)
			{
				return "FailedToAdd";
			}

		}
		public async Task<string> EditAsync(Instructor Instructor, IFormFile file)
		{
			var OldUrl = Instructor.Image;
			//	var path = "D:\\Desktop\\SchoolProjectWithCleanArchitecture.Api\\SchoolProject.Api\\wwwroot\\";
			//var path = Path.Combine(_webHost.WebRootPath, OldUrl!);
			var UrlRoot = _webHost.WebRootPath;
			var path = $"{UrlRoot}{OldUrl}";
			var imageUrl = await _fileService.UploadImage("Instructor", file);
			if (OldUrl != null)
			{
				System.IO.File.Delete(path);
			}
			Instructor.Image = imageUrl;
			switch (imageUrl)
			{
				case "NoImage": return "NoImage";
				case "FailedToUploadImage": return "FailedToUpdateImage";
			}
			try
			{
				await _instructorRepository.UpdateAsync(Instructor);
				return "Success";
			}
			catch
			{
				return "FailedToUpdate";
			}
		}
		public async Task<string> DeleteAsync(Instructor Instructor)
		{
			var transaction = await _instructorRepository.BeginTransactionAsync();
			try
			{
				var OldUrl = Instructor.Image!;
				var UrlRoot = _webHost.WebRootPath;
				var path = $"{UrlRoot}{OldUrl}";
				await _instructorRepository.DeleteAsync(Instructor);
				System.IO.File.Delete(path);
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
			var instructor = await _instructorRepository.GetTableNoTracking()
												  .Where(x => x.Phone!.Equals(phone))
												  .FirstOrDefaultAsync();
			if (instructor == null) { return false; }
			else return true;
		}
		public async Task<bool> IsPhoneExcludeSelf(string phone, int id)
		{
			var instructor = await _instructorRepository.GetTableNoTracking()
												  .Where(x => x.Phone!.Equals(phone) && !x.InstructorId.Equals(id))
												  .FirstOrDefaultAsync();
			if (instructor == null) { return false; }
			else return true;
		}
		public async Task<bool> IsSupervisorIdExist(int supervisorId)
		{
			var SupervisorId = await _instructorRepository.GetTableNoTracking()
												  .AnyAsync(x => x.SuperVisorId!.Equals(supervisorId));
			return SupervisorId;
		}
		public async Task<bool> IsDepartmentManagerIdExist(int departmentManagerId)
		{
			var ManagerId = await _instructorRepository.GetTableNoTracking()
												  .AnyAsync(x => x.InstructorId.Equals(departmentManagerId));
			return ManagerId;
		}
		#endregion
	}
}
