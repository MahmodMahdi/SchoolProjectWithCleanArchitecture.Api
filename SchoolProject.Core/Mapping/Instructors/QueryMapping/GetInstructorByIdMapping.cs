using AutoMapper;
using SchoolProject.Core.Features.Instructors.Queries.Dtos;
using SchoolProject.Data.Entities;
namespace SchoolProject.Core.Mapping.Instructors
{
	public partial class InstructorProfile : Profile
	{
		public void GetInstructorByIdMapping()
		{
			CreateMap<Instructor, GetInstructorResponse>()
				.ForMember(dest => dest.Id, op => op.MapFrom(src => src.InstructorId))
					.ForMember(dest => dest.Image, op => op.MapFrom(src => src.Image))
				.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)))
				.ForMember(dest => dest.SuperVisor, op => op.MapFrom(src => src.Supervisor!.Localize(src.Supervisor.NameAr!, src.Supervisor.NameEn!)))
				.ForMember(dest => dest.Department, op => op.MapFrom(src => src.Localize(src.departmentManager!.DepartmentNameAr!, src.departmentManager!.DepartmentNameEn!)))
				.ForMember(dest => dest.DepartmentManager, op => op.MapFrom(src => src.Localize(src.department!.DepartmentNameAr!, src.department!.DepartmentNameEn!)))
				.ForMember(dest => dest.SubjectList, op => op.MapFrom(src => src.InstructorSubjects))
				.ForMember(dest => dest.SuperVisorOn, op => op.MapFrom(src => src.Instructors));

			CreateMap<InstructorSubject, SubjectsResponse>()
				.ForMember(dest => dest.Id, op => op.MapFrom(src => src.SubjectId))
				.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subject!.Localize(src.Subject.SubjectNameAr!, src.Subject.SubjectNameEn!)));

			CreateMap<Instructor, InstructorsResponse>()
			   .ForMember(dest => dest.Id, op => op.MapFrom(src => src.InstructorId))
			   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
