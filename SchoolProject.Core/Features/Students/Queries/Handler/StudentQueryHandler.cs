using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Dtos;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Queries.Handlers
{
	public class StudentQueryHandler : ResponseHandler,
									   IRequestHandler<GetStudentListQuery, Response<List<GetStudentListResponse>>>,
									   IRequestHandler<GetStudentByIdQuery, Response<GetStudentResponse>>,
									   IRequestHandler<GetStudentPaginatedListQuery, PaginateResult<GetStudentPaginatedListResponse>>
	{
		// Mediator
		#region Fields
		private readonly IStudentService _studentService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructors
		public StudentQueryHandler(IStudentService studentService,
								   IMapper mapper,
								   IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			this._studentService = studentService;
			this._mapper = mapper;
			this._stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
		{
			var StudentList = await _studentService.GetStudentsListAsync();
			var StudentListMapper = _mapper.Map<List<GetStudentListResponse>>(StudentList);
			var result = Success(StudentListMapper);
			result.Meta = new { Count = StudentListMapper.Count };
			return result;
		}

		public async Task<Response<GetStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
		{
			var Student = await _studentService.GetStudentByIdWithIncludeAsync(request.Id);
			if (Student == null)
			{
				// using the localization
				return NotFound<GetStudentResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
			}
			var result = _mapper.Map<GetStudentResponse>(Student);
			return Success(result);
		}

		public async Task<PaginateResult<GetStudentPaginatedListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
		{
			// delegate (first way)
			//Expression<Func<Student, GetStudentPaginatedListResponse>> expression = (e => new GetStudentPaginatedListResponse(e.Id,
			// e.Localize(e.NameAr!, e.NameEn!), e.Address!, e.Age,
			// e.Localize(e.Department!.DepartmentNameAr!, e.Department!.DepartmentNameEn!)));
			var FilterQuery = _studentService.FilterStudentPaginatedQueryable(request.OrderBy, request.Search!);

			// second way
			//await FilterQuery.Select(x => new GetStudentPaginatedListResponse(x.Id, x.Localize(x.NameAr!, x.NameEn!), x.Address!, x.Age, x.Department!.Localize(x.Department.DepartmentNameAr!, x.Department.DepartmentNameEn!)))
			var paginatedList = await _mapper.ProjectTo<GetStudentPaginatedListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
			paginatedList.Meta = new { Count = paginatedList.Data!.Count };
			return paginatedList;

		}
		#endregion
	}
}
