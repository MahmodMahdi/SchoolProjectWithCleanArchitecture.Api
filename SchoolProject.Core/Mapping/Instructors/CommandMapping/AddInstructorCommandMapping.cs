using AutoMapper;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
	public partial class InstructorProfile : Profile
	{
		public void AddInstructorCommandMapping()
		{
			CreateMap<AddInstructorCommand, Instructor>()
				.ForMember(dest => dest.Image, op => op.Ignore())
				.ForMember(dest => dest.NameAr, op => op.MapFrom(src => src.InstructorNameAr))
				.ForMember(dest => dest.NameEn, op => op.MapFrom(src => src.InstructorNameEn))
				.ForMember(dest => dest.SuperVisorId, op => op.MapFrom(src => src.SuperVisorId))
				.ForMember(dest => dest.DepartmentId, op => op.MapFrom(src => src.DepartmentId));


		}
	}
}