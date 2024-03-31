using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Infrastructure.Context;
using SchoolProject.Infrastructure.InfrastructureBases;
using SchoolProject.Infrastructure.Interfaces;

namespace SchoolProject.Infrastructure.Repositories
{
	public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
	{
		#region fields
		private readonly DbSet<UserRefreshToken> _userRefreshToken;
		#endregion
		#region Constructors
		public RefreshTokenRepository(ApplicationDBContext _context) : base(_context)
		{
			_userRefreshToken = _context.Set<UserRefreshToken>();
		}
		#endregion
	}
}
