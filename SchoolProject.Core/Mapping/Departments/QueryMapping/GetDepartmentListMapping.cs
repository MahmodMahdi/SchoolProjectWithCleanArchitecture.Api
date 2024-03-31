using SchoolProject.Core.Features.Departments.Queries.Dtos;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
	public partial class DepartmentProfile
	{
		public void GetDepartmentListMapping()
		{
			CreateMap<Department, GetDepartmentListResponse>()
				.ForMember(dest => dest.Id, op => op.MapFrom(src => src.DepartmentID))
				.ForMember(dest => dest.Name, op => op.MapFrom(src => src.Localize(src.DepartmentNameAr!, src.DepartmentNameEn!)))
				.ForMember(dest => dest.ManagerName, op => op.MapFrom(src => src.Instructor!.Localize(src.Instructor.NameAr!, src.Instructor.NameEn!)));
		}
	}
}