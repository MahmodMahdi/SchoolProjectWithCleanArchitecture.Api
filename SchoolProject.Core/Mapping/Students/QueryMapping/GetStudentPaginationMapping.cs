using SchoolProject.Core.Features.Students.Queries.Dtos;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
	public partial class StudentProfile
	{
		public void GetStudentPaginationMapping()
		{
			CreateMap<Student, GetStudentPaginatedListResponse>()
		   .ForMember(dest => dest.DepartmentName, op => op.MapFrom(src => src.Localize(src.Department!.DepartmentNameAr!, src.Department!.DepartmentNameEn!)))
		   .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)))
		   .ForMember(dest => dest.Id, op => op.MapFrom(src => src.Id))
		   .ForMember(dest => dest.Address, op => op.MapFrom(src => src.Address))
		   .ForMember(dest => dest.Age, op => op.MapFrom(src => src.Age));
		}
	}
}
