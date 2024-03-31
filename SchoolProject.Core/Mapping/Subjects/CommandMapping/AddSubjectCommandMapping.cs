using AutoMapper;
using SchoolProject.Core.Features.Subjects.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subjects
{
	public partial class SubjectProfile : Profile
	{
		public void AddSubjectCommandMapping()
		{
			CreateMap<AddSubjectCommand, Subject>()
			.ForMember(dest => dest.SubjectNameAr, op => op.MapFrom(src => src.NameAr!))
			.ForMember(dest => dest.SubjectNameEn, op => op.MapFrom(src => src.NameEn!));
		}
	}
}
