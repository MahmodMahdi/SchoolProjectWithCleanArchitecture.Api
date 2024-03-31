using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Queries.Dtos;
using SchoolProject.Core.Features.Instructors.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Queries.Handlers
{
	public class InstructorQueryHandler : ResponseHandler,
		 IRequestHandler<GetInstructorListQuery, Response<List<GetInstructorListResponse>>>,
		IRequestHandler<GetInstructorByIdQuery, Response<GetInstructorResponse>>,
		IRequestHandler<GetInstructorOnlyByIdQuery, Response<GetInstructorOnlyResponse>>
	{
		#region Fields
		private readonly IStringLocalizer _stringLocalizer;
		private readonly IMapper _mapper;
		private readonly IInstructorService _instructorService;
		#endregion

		#region Constructor
		public InstructorQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
									  IMapper mapper,
									  IInstructorService instructorService) : base(stringLocalizer)
		{
			_stringLocalizer = stringLocalizer;
			_mapper = mapper;
			_instructorService = instructorService;
		}
		#endregion

		#region Handle functions
		public async Task<Response<List<GetInstructorListResponse>>> Handle(GetInstructorListQuery request, CancellationToken cancellationToken)
		{
			var response = await _instructorService.GetInstructorList();
			// Check is not exist 
			if (response == null) return NotFound<List<GetInstructorListResponse>>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// Mapping
			var instructor = _mapper.Map<List<GetInstructorListResponse>>(response);
			var result = Success(instructor);
			result.Meta = new { Count = instructor.Count };
			return result;
		}
		public async Task<Response<GetInstructorResponse>> Handle(GetInstructorByIdQuery request, CancellationToken cancellationToken)
		{
			// service GetStudent  include (stud,Ins,Sub)
			var response = await _instructorService.GetInstructorById(request.Id);
			// Check is not exist 
			if (response == null) return NotFound<GetInstructorResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// Mapping
			var instructor = _mapper.Map<GetInstructorResponse>(response);

			//return response
			return Success(instructor);
		}

		public async Task<Response<GetInstructorOnlyResponse>> Handle(GetInstructorOnlyByIdQuery request, CancellationToken cancellationToken)
		{
			var Ins = new Instructor();
			// service GetStudent  include (stud,Ins,Sub)
			var response = await _instructorService.GetInstructorOnlyById(request.Id);
			// Check is not exist 
			if (response == null) return NotFound<GetInstructorOnlyResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

			// Mapping
			var instructor = _mapper.Map<GetInstructorOnlyResponse>(response);

			//return response
			return Success(instructor);
		}
		#endregion
	}
}


