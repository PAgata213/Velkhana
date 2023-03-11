using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Velkhana.Shared.Domain.Common;

namespace Velkhana.Shared.Infrastructure.Persistence;
public class RepositoryBase<T> : IRepository<T> 
  where T : EntityBase
{
  private readonly DbContext _dbContext;

  public RepositoryBase(DbContext dbContext)
  {
    _dbContext = dbContext;
  }

  public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predictate)
    => await _dbContext.Set<T>().AnyAsync(predictate);

  public async Task<IList<T>> GetAllAsync()
    => await _dbContext.Set<T>().ToListAsync();

  public async Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> predictate)
    => await _dbContext.Set<T>().Where(predictate).ToListAsync();

  public async Task<T?> GetAsync(Expression<Func<T, bool>> predictate) 
    => await _dbContext.Set<T>().FirstOrDefaultAsync(predictate);

  public async Task<T?> GetByIdAsync(Guid id)
    => await GetAsync(s => s.Id == id);

  public async Task AddAsync(T entity)
    => await _dbContext.Set<T>().AddAsync(entity);

  public void Remove(T entity)
    => _dbContext.Set<T>().Remove(entity);

  public async Task RemoveWithId(Guid id) 
  {
    var entity = await GetByIdAsync(id);
    if(entity == null)
    {
      return;
    }
    Remove(entity);
  }


  public async Task<int> SaveChangesAsync()
    => await _dbContext.SaveChangesAsync();
}