using SchoolProject.Core.Features.Students.Queries.Dtos;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students
{
	public partial class StudentProfile
	{
		public void GetStudentByIdMapping()
		{
			CreateMap<Student, GetStudentResponse>()
		   .ForMember(dest => dest.Department, op => op.MapFrom(src => src.Department!.Localize(src.Department.DepartmentNameAr!, src.Department.DepartmentNameEn!)))
			.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));
		}
	}
}
