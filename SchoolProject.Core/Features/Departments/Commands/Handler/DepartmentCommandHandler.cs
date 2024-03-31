using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
namespace SchoolProject.Core.Features.Departments.Commands.Handlers
{
	public class DepartmentCommandHandler : ResponseHandler,
										IRequestHandler<AddDepartmentCommand, Response<string>>,
										IRequestHandler<UpdateDepartmentCommand, Response<string>>,
										IRequestHandler<DeleteDepartmentCommand, Response<string>>
	{
		#region Fields
		private readonly IDepartmentService _departmentService;
		private readonly IMapper _mapper;
		private readonly IStringLocalizer<SharedResources> _stringLocalizer;
		#endregion
		#region Constructor
		public DepartmentCommandHandler(IDepartmentService departmentService,
									 IMapper mapper,
									 IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
		{
			_departmentService = departmentService;
			_mapper = mapper;
			_stringLocalizer = stringLocalizer;
		}
		#endregion
		#region Handle Functions
		public async Task<Response<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
		{
			// mapping
			var studentMapper = _mapper.Map<Department>(request);
			// Add
			var result = await _departmentService.AddAsync(studentMapper);
			// return response
			if (result == "Success") return Created("Added Successfully");
			else return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var department = await _departmentService.GetDepartment(request.DepartmentId);
			// return notFound
			if (department == null) return NotFound<string>("Department is not found");
			// mapping 
			var departmentMapper = _mapper.Map(request, department);
			// call service 
			var result = await _departmentService.EditAsync(departmentMapper);
			//return response
			if (result == "Success") return Success($"Updated Successfully {departmentMapper.DepartmentID}");
			else return BadRequest<string>();
		}

		public async Task<Response<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
		{
			// check if the id is exist or not 
			var department = await _departmentService.GetDepartmentById(request.Id);
			// return notFound
			if (department == null) return NotFound<string>("Department is not found");
			// call service 
			var result = await _departmentService.DeleteAsync(department);
			if (result == "Success") return Deleted<string>($"{request.Id} Deleted Successfully ");
			else return BadRequest<string>();
		}



		#endregion
	}
}
