using AutoMapper;
using SchoolProject.Core.Features.Subjects.Queries.Dtos;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subjects
{
	public partial class SubjectProfile : Profile
	{
		public void GetSubjectOnlyByIdMapping()
		{
			CreateMap<Subject, GetSubjectOnlyResponse>()
			  .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.SubjectNameAr!, src.SubjectNameEn!)));
		}
	}
}
