using AutoMapper;
using SchoolProject.Core.Features.Departments.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public void UpdateDepartmentCommandMapping()
        {
            CreateMap<UpdateDepartmentCommand, Department>()
            .ForMember(dest => dest.DepartmentManager, op => op.MapFrom(src => src.DepartmentManager))
            .ForMember(dest => dest.DepartmentNameAr, op => op.MapFrom(src => src.DepartmentNameAr))
            .ForMember(dest => dest.DepartmentNameEn, op => op.MapFrom(src => src.DepartmentNameEn))
            .ForMember(dest => dest.DepartmentID, op => op.MapFrom(src => src.DepartmentId));
        }
    }
}