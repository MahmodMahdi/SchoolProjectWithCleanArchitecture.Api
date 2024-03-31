﻿using AutoMapper;

namespace SchoolProject.Core.Mapping.ApplicationUser
{
    public partial class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            GetUserPaginatedMapping();
            GetUserByIdMapping();
            AddUserMapping();
            UpdateUserMapping();
        }

    }
}
