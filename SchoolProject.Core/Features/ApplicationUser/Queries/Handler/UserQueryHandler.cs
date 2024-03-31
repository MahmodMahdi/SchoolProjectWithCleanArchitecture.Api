using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.ApplicationUser.Queries.Dtos;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Features.ApplicationUser.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginationQuery, PaginateResult<GetUsersPaginatedResponse>>,
         IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly UserManager<User> _userManager;
        #endregion
        #region Constructor
        public UserQueryHandler(IMapper mapper,
                                  IStringLocalizer<SharedResources> stringLocalizer,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }
        #endregion
        #region Handle Function

        public async Task<PaginateResult<GetUsersPaginatedResponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var PaginatedUsers = await _mapper.ProjectTo<GetUsersPaginatedResponse>(users).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return PaginatedUsers;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null)
            {
                return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            }
            var UserMapped = _mapper.Map<GetUserByIdResponse>(user);
            return Success(UserMapped);
        }


        #endregion
    }

}
