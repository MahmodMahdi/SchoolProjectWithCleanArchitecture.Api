using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.InfrastructureBases;

namespace SchoolProject.Infrastructure.Interfaces
{
	public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
	{
	}
}
