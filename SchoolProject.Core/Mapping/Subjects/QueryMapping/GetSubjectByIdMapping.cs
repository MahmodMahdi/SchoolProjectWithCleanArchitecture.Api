using AutoMapper;
using SchoolProject.Core.Features.Subjects.Queries.Dtos;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Subjects
{
    public partial class SubjectProfile : Profile
    {
        public void GetSubjectByIdMapping()
        {
            CreateMap<Subject, GetSubjectResponse>()
                .ForMember(dest => dest.SubjectID, op => op.MapFrom(src => src.SubjectID))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.SubjectNameAr!, src.SubjectNameEn!)))
                .ForMember(dest => dest.DepartmentList, op => op.MapFrom(src => src.DepartmentsSubjects))
                .ForMember(dest => dest.InstructorList, op => op.MapFrom(src => src.InstructorSubjects))
                .ForMember(dest => dest.StudentSList, op => op.MapFrom(src => src.StudentsSubjects));

            CreateMap<DepartmentSubject, DepartmentSResponse>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.DepartmentID))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Department!
                .Localize(src.Department!.DepartmentNameAr!, src.Department.DepartmentNameEn!)));


            CreateMap<InstructorSubject, InstructorsResponse>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.InstructorId))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.instructor!
                .Localize(src.instructor.NameAr!, src.instructor.NameEn!)));

            CreateMap<StudentSubject, StudentSResponse>()
                .ForMember(dest => dest.Id, op => op.MapFrom(src => src.StudentID))
                .ForMember(dest => dest.Name, op => op.MapFrom(src => src.Student!
                .Localize(src.Student.NameAr!, src.Student.NameEn!)));
        }
    }
}

