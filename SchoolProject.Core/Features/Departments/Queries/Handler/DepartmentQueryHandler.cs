using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Queries.Dtos;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;
namespace SchoolProject.Core.Features.Departments.Queries.Handlers
{
	public class DepartmentQueryHandler : ResponseHandler,
									   IRequestHandler<GetDepartmentByIdQuery, Response<GetDepartmentResponse>>,
									   IRequestHandler<GetDepartmentOnlyByIdQuery, Response<GetDepartmentOnlyResponse>>,
									   IRequestHandler<GetDepartmentListQuery, Response<List<GetDepartmentListResponse>>>
	{
		#region Fields
		private readonly IStringLocalizer _stringLocalizer;
		private readonly IMapper _mapper;
		private readonly IDepartmentService _departmentService;
		private readonly IStudentService _studentService;
		#endregion

		#region Constructor
		public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
									  IMapper mapper,
									  IDepartmentService departmentService,
									  IStudentService studentService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_mapper = mapper;
			_departmentService = departmentService;
			_studentService = studentService;
		}




		#endregion

		#region Handle Functions

		public async Task<Response<GetDepartmentResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
		{
			// service GetStudent  include (stud,Ins,Sub)
			var response = await _departmentService.GetDepartmentById(request.Id);
			// Check is not exist 
			if (response == null) return NotFound<GetDepartmentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// Mapping
			var department = _mapper.Map<GetDepartmentResponse>(response);
			// Pagination 
			Expression<Func<Student, StudentResponse>> expression = (e => new StudentResponse(e.Id,
		  														 e.Localize(e.NameAr!, e.NameEn!)));
			var studentQueryable = _studentService.GetStudentsQuerableByDepartmentId(request.Id);
			var paginatedList = await studentQueryable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
			paginatedList.Meta = new { Count = paginatedList.Data!.Count };
			department.StudentList = paginatedList;
			//return response
			return Success(department);
		}

		public async Task<Response<GetDepartmentOnlyResponse>> Handle(GetDepartmentOnlyByIdQuery request, CancellationToken cancellationToken)
		{
			// service GetStudent  include (stud,Ins,Sub)
			var response = await _departmentService.GetDepartmentOnlyById(request.Id);
			// Check is not exist 
			if (response == null) return NotFound<GetDepartmentOnlyResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// Mapping
			var department = _mapper.Map<GetDepartmentOnlyResponse>(response);

			//return response
			return Success(department);
		}

		public async Task<Response<List<GetDepartmentListResponse>>> Handle(GetDepartmentListQuery request, CancellationToken cancellationToken)
		{
			var response = await _departmentService.GetDepartmentList();
			// Check is not exist 
			if (response == null) return NotFound<List<GetDepartmentListResponse>>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// Mapping
			var department = _mapper.Map<List<GetDepartmentListResponse>>(response);
			var result = Success(department);
			result.Meta = new { Count = department.Count };
			return result;
		}
		#endregion
	}
}
