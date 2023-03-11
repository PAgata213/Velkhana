﻿using System.Linq.Expressions;

namespace Velkhana.Shared.Domain.Common;
public interface IRepository<T> where T : EntityBase
{
  Task<IList<T>> GetAllAsync();
  Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> predictate);
  Task<T?> GetAsync(Expression<Func<T, bool>> predictate);
  Task AddAsync(T entity);
  void Remove(T entity);
  Task<int> SaveChangesAsync();
}
