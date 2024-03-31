using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
                                        IRequestHandler<AddStudentCommand, Response<string>>,
                                        IRequestHandler<UpdateStudentCommand, Response<string>>,
                                        IRequestHandler<DeleteStudentCommand, Response<string>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        #endregion
        #region Constructor
        public StudentCommandHandler(IStudentService studentService,
                                     IMapper mapper,
                                     IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            // mapping
            var studentMapper = _mapper.Map<Student>(request);
            // Add
            var result = await _studentService.AddAsync(studentMapper);

            // return response
            if (result == "Success") return Created("Added Successfully");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            // check if the id is exist or not 
            var student = await _studentService.GetByIdAsync(request.Id);
            // return notFound
            if (student == null) return NotFound<string>("Student is not found");
            // mapping 
            var studentMapper = _mapper.Map(request, student);
            // call service 
            var result = await _studentService.EditAsync(studentMapper);
            //return response
            if (result == "Success") return Success($"Updated Successfully{studentMapper.Id}");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            // check if the id is exist or not 
            var student = await _studentService.GetByIdAsync(request.Id);
            // return notFound
            if (student == null) return NotFound<string>("Student is not found");
            // call service 
            var result = await _studentService.DeleteAsync(student);
            if (result == "Success") return Deleted<string>($"{request.Id} Deleted Successfully ");
            else return BadRequest<string>();
        }
        #endregion
    }
}
