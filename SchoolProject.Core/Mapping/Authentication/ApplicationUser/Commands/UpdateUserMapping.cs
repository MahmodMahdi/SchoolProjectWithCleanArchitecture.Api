using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, User>()
            .ForMember(dest => dest.Email, op => op.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, op => op.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, op => op.MapFrom(src => src.Phone));
        }
    }
}
