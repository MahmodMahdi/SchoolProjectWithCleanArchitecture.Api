using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Subjects.Queries.Dtos;
using SchoolProject.Core.Features.Subjects.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Service.Abstracts;

namespace SchoolProject.Core.Features.Subjects.Queries.Handler
{
    internal class SubjectQueryHandler : ResponseHandler,
                                       IRequestHandler<GetSubjectListQuery, Response<List<GetSubjectListResponse>>>,
                                       IRequestHandler<GetSubjectOnlyByIdQuery, Response<GetSubjectOnlyResponse>>,
                                       IRequestHandler<GetSubjectByIdQuery, Response<GetSubjectResponse>>
    {
        #region Fields
        private readonly IStringLocalizer _stringLocalizer;
        private readonly IMapper _mapper;
        private readonly ISubjectService _subjectService;
        #endregion

        #region Constructor
        public SubjectQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                      IMapper mapper,
                                      ISubjectService subjectService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _mapper = mapper;
            _subjectService = subjectService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetSubjectListResponse>>> Handle(GetSubjectListQuery request, CancellationToken cancellationToken)
        {
            var response = await _subjectService.GetSubjectListAsync();
            // Check is not exist 
            if (response == null) return NotFound<List<GetSubjectListResponse>>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            // Mapping
            var subjects = _mapper.Map<List<GetSubjectListResponse>>(response);
            var result = Success(subjects);
            result.Meta = new { Count = subjects.Count };
            return result;
        }

        public async Task<Response<GetSubjectOnlyResponse>> Handle(GetSubjectOnlyByIdQuery request, CancellationToken cancellationToken)
        {
            // service GetStudent  include (stud,Ins,Sub)
            var response = await _subjectService.GetSubjectOnlyById(request.Id);
            // Check is not exist 
            if (response == null) return NotFound<GetSubjectOnlyResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            // Mapping
            var subject = _mapper.Map<GetSubjectOnlyResponse>(response);

            //return response
            return Success(subject);
        }

        public async Task<Response<GetSubjectResponse>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
        {
            // service GetStudent  include (stud,Ins,Sub)
            var response = await _subjectService.GetSubjectById(request.Id);
            // Check is not exist 
            if (response == null) return NotFound<GetSubjectResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            // Mapping
            var subject = _mapper.Map<GetSubjectResponse>(response);

            //return response
            return Success(subject);
        }
        #endregion
    }
}
