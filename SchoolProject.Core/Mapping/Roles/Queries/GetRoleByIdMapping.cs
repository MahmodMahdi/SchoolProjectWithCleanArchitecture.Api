using SchoolProject.Core.Features.Authorization.Queries.Dtos;
using SchoolProject.Data.Entities.Identity;

namespace SchoolProject.Core.Mapping.Roles
{
	public partial class RoleProfile
	{
		public void GetRoleByIdMapping()
		{
			CreateMap<Role, GetRoleByIdResponse>();
		}
	}
}
