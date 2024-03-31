using SchoolProject.Core.Features.Departments.Queries.Dtos;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
	public partial class DepartmentProfile
	{
		public void GetDepartmentByIdMapping()
		{
			CreateMap<Department, GetDepartmentResponse>()
				.ForMember(dest => dest.Id, op => op.MapFrom(src => src.DepartmentID))
				.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.DepartmentNameAr!, src.DepartmentNameEn!)))
				.ForMember(dest => dest.ManagerName, op => op.MapFrom(src => src.Instructor!.Localize(src.Instructor.NameAr!, src.Instructor.NameEn!)))
				.ForMember(dest => dest.SubjectList, op => op.MapFrom(src => src.DepartmentSubjects))
				.ForMember(dest => dest.InstructorList, op => op.MapFrom(src => src.Instructors));

			// i make it paginated so i don't need it
			//.ForMember(dest => dest.StudentList, op => op.MapFrom(src => src.Students));

			CreateMap<DepartmentSubject, SubjectResponse>()
				.ForMember(dest => dest.Id, op => op.MapFrom(src => src.SubjectID))
				.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Subject!.Localize(src.Subject.SubjectNameAr!, src.Subject.SubjectNameEn!)));

			// i make it paginated so i don't need it
			//CreateMap<Student, StudentResponse>()
			//	.ForMember(dest => dest.ID, op => op.MapFrom(src => src.Id))
			//	.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));

			CreateMap<Instructor, InstructorResponse>()
				.ForMember(dest => dest.Id, op => op.MapFrom(src => src.InstructorId))
				.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.NameAr!, src.NameEn!)));

		}
	}
}
