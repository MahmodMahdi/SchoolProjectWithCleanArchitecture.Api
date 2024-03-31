
using AutoMapper;
using SchoolProject.Core.Features.Instructors.Queries.Dtos;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
	public partial class InstructorProfile : Profile
	{
		public void GetInstructorOnlyByIdMapping()
		{
			CreateMap<Instructor, GetInstructorOnlyResponse>()
				.ForMember(dest => dest.Id, op => op.MapFrom(src => src.InstructorId))
				.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)))
				.ForMember(dest => dest.SuperVisor, op => op.MapFrom(src => src.Supervisor!.Localize(src.Supervisor.NameAr!, src.Supervisor.NameEn!)))
				.ForMember(dest => dest.Image, op => op.MapFrom(src => src.Image))
				.ForMember(dest => dest.Department, op => op.MapFrom(src => src.Localize(src.departmentManager!.DepartmentNameAr!, src.departmentManager!.DepartmentNameEn!)))
				.ForMember(dest => dest.DepartmentManager, op => op.MapFrom(src => src.Localize(src.department!.DepartmentNameAr!, src.department!.DepartmentNameEn!)));
		}
	}
}
