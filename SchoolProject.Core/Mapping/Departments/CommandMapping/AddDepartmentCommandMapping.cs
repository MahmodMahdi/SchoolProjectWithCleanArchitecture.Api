using AutoMapper;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
	public partial class DepartmentProfile : Profile
	{
		public void AddDepartmentCommandMapping()
		{
			CreateMap<AddDepartmentCommand, Department>()
			.ForMember(dest => dest.DepartmentManager, op => op.MapFrom(src => src.DepartmentManager))
			.ForMember(dest => dest.DepartmentNameAr, op => op.MapFrom(src => src.DepartmentNameAr))
			.ForMember(dest => dest.DepartmentNameEn, op => op.MapFrom(src => src.DepartmentNameEn));
		}
	}
}
