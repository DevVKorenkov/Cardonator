using Cardonator.Models.Abstraction;
using Cardonator.Models.Models;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cardonator.Data.Repositories.Abstrations;

public interface IRepository<T> where T : class
{
    Task<RepositoryResult> AddAsync(T item);
    Task<T> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
    Task<RepositoryResult> UpdateAsync(T item);
    Task<RepositoryResult> RemoveAsync(int id);
    Task<RepositoryResult> SaveAsync();
}
