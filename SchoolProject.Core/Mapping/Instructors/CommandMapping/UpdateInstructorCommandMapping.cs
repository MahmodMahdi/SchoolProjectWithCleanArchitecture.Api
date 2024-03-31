using AutoMapper;
using SchoolProject.Core.Features.Instructors.Commands.Models;
using SchoolProject.Data.Entities;

namespace SchoolProject.Core.Mapping.Instructors
{
	public partial class InstructorProfile : Profile
	{
		public void UpdateInstructorCommandMapping()
		{
			CreateMap<UpdateInstructorCommand, Instructor>()
				.ForMember(dest => dest.Image, op => op.Ignore())
				.ForMember(dest => dest.InstructorId, op => op.MapFrom(src => src.InstructorId))
				.ForMember(dest => dest.NameAr, op => op.MapFrom(src => src.InstructorNameAr))
				.ForMember(dest => dest.NameEn, op => op.MapFrom(src => src.InstructorNameEn));
		}
	}
}