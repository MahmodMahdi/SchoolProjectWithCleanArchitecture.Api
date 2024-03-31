using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SchoolProject.Infrastructure.Context;

namespace SchoolProject.Infrastructure.InfrastructureBases
{
	public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
	{

		#region Fields

		protected readonly ApplicationDBContext _dbContext;

		#endregion

		#region Constructor
		public GenericRepositoryAsync(ApplicationDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		#endregion

		#region Actions
		public virtual async Task<T> GetByIdAsync(int id)
		{

			return (await _dbContext.Set<T>().FindAsync(id))!;
		}


		public IQueryable<T> GetTableNoTracking()
		{
			return _dbContext.Set<T>().AsNoTracking().AsQueryable();
		}


		public virtual async Task AddRangeAsync(ICollection<T> entities)
		{
			await _dbContext.Set<T>().AddRangeAsync(entities);
			await _dbContext.SaveChangesAsync();

		}
		public virtual async Task<T> AddAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			await _dbContext.SaveChangesAsync();

			return entity;
		}

		public virtual async Task UpdateAsync(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			await _dbContext.SaveChangesAsync();

		}

		public virtual async Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			await _dbContext.SaveChangesAsync();
		}
		public virtual async Task DeleteRangeAsync(ICollection<T> entities)
		{
			foreach (var entity in entities)
			{
				_dbContext.Entry(entity).State = EntityState.Deleted;
			}
			await _dbContext.SaveChangesAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}



		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{


			return await _dbContext.Database.BeginTransactionAsync();
		}

		public async Task CommitAsync()
		{
			await _dbContext.Database.CommitTransactionAsync();

		}

		public async Task RollBackAsync()
		{
			await _dbContext.Database.RollbackTransactionAsync();

		}

		public IQueryable<T> GetTableAsTracking()
		{
			return _dbContext.Set<T>().AsQueryable();

		}

		public virtual async Task UpdateRangeAsync(ICollection<T> entities)
		{
			_dbContext.Set<T>().UpdateRange(entities);
			await _dbContext.SaveChangesAsync();
		}
		#endregion
	}
}
