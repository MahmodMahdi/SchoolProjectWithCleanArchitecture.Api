using SchoolProject.Core.Features.ApplicationUser.Queries.Dtos;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile
    {
        public void GetUserPaginatedMapping()
        {
            CreateMap<User, GetUsersPaginatedResponse>();

        }
    }
}
