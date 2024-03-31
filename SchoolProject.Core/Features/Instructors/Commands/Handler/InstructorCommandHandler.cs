using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Instructors.Commands.Handler
{
	public class InstructorCommandHandler : ResponseHandler,
										IRequestHandler<AddInstructorCommand, Response<string>>,
										IRequestHandler<UpdateInstructorCommand, Response<string>>,
										IRequestHandler<DeleteInstructorCommand, Response<string>>
	{
		#region Fields
		private readonly IInstructorService _instructorService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public InstructorCommandHandler(IInstructorService instructorService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_instructorService = instructorService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
		{
			// mapping
			var instructorMapper = _mapper.Map<Instructor>(request);
			// Add
			var result = await _instructorService.AddAsync(instructorMapper, request.Image!);
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUploadImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUploadImage]);
				case "FailedToAdd": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAdd]);
			}
			return Created("Added Successfully");
		}

		public async Task<Response<string>> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var instructor = await _instructorService.GetInstructor(request.InstructorId);
			// return notFound
			if (instructor == null) return NotFound<string>("Instructor is not found");
			// mapping 
			var instructorMapper = _mapper.Map(request, instructor);
			// call service 
			var result = await _instructorService.EditAsync(instructorMapper, request.Image!);
			//return response
			switch (result)
			{
				case "NoImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.NoImage]);
				case "FailedToUpdateImage": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateImage]);
				case "FailedToUpdate": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdate]);
			}
			return Success($"{instructorMapper.InstructorId} Updated Successfully");
		}

		public async Task<Response<string>> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var instructor = await _instructorService.GetInstructor(request.Id);
			// return notFound
			if (instructor == null) return NotFound<string>("Instructor is not found");
			// call service 
			var result = await _instructorService.DeleteAsync(instructor);
			if (result == "Success") return Deleted<string>($"{request.Id} Deleted Successfully ");
			else return BadRequest<string>();
		}


		#endregion
	}
}
