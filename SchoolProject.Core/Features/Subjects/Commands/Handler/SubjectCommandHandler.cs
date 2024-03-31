using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Subjects.Commands.Handler
{
	public class SubjectCommandHandler : ResponseHandler,
										IRequestHandler<AddSubjectCommand, Response<string>>,
										IRequestHandler<UpdateSubjectCommand, Response<string>>,
										IRequestHandler<DeleteSubjectCommand, Response<string>>
	{
		#region Fields
		private readonly ISubjectService _subjectService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public SubjectCommandHandler(ISubjectService subjectService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_subjectService = subjectService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddSubjectCommand request, CancellationToken cancellationToken)
		{
			// mapping
			var subjectMapper = _mapper.Map<Subject>(request);
			// Add
			var result = await _subjectService.AddAsync(subjectMapper);
			// return response
			if (result == "Success") return Created("Added Successfully");
			else return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var subject = await _subjectService.GetSubjectById(request.Id);
			// return notFound
			if (subject == null) return NotFound<string>("Subject is not found");
			// mapping 
			var subjectMapper = _mapper.Map(request, subject);
			// call service 
			var result = await _subjectService.EditAsync(subjectMapper);
			//return response
			if (result == "Success") return Success($"{subjectMapper.SubjectID} Updated Successfully");
			else return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var subject = await _subjectService.GetSubjectById(request.Id);
			// return notFound
			if (subject == null) return NotFound<string>("Subject is not found");
			// call service 
			var result = await _subjectService.DeleteAsync(subject);
			if (result == "Success") return Deleted<string>($"{request.Id} Deleted Successfully ");
			else return BadRequest<string>();
		}
		#endregion
	}
}
