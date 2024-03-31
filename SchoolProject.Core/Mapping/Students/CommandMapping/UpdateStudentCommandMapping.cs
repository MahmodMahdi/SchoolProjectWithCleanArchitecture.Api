using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Students;
public partial class StudentProfile
{
    public void UpdateStudentCommandMapping()
    {
        CreateMap<UpdateStudentCommand, Student>()
       .ForMember(dest => dest.DepartmentID, op => op.MapFrom(src => src.DepartmentId))
        .ForMember(dest => dest.NameAr, op => op.MapFrom(src => src.NameAr))
        .ForMember(dest => dest.NameEn, op => op.MapFrom(src => src.NameEn))
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}
