using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students;
public partial class StudentProfile
{
	public void AddStudentCommandMapping()
	{
		CreateMap<AddStudentCommand, Student>()
	   .ForMember(dest => dest.DepartmentID, op => op.MapFrom(src => src.DepartmentId))
		.ForMember(dest => dest.NameAr, op => op.MapFrom(src => src.NameAr))
		.ForMember(dest => dest.NameEn, op => op.MapFrom(src => src.NameEn));
	}
}
