using Cardonator.Data.DataContext;
using Cardonator.Data.Repositories.Abstrations;
using Cardonator.Models.Abstraction;
using Cardonator.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cardonator.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CardsDataContext _context;
    private readonly DbSet<CardonatorUser> _users;
    private readonly ISaveContext _saveContext;
    public UserRepository(
        CardsDataContext cardsDataContext, 
        ISaveContext saveContext)
    {
        _context = cardsDataContext;
        _users = _context.Set<CardonatorUser>();
        _saveContext = saveContext;
    }

    public Task<RepositoryResult> AddAsync(CardonatorUser item)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CardonatorUser>> GetAllAsync(
        Expression<Func<CardonatorUser, bool>> filter = null, 
        Func<IQueryable<CardonatorUser>, IIncludableQueryable<CardonatorUser, object>> includes = null)
    {
        throw new NotImplementedException();
    }

    public async Task<CardonatorUser> GetAsync(
        Expression<Func<CardonatorUser, bool>> filter = null, 
        Func<IQueryable<CardonatorUser>, IIncludableQueryable<CardonatorUser, object>> includes = null)
    {
        IQueryable<CardonatorUser> query = _users;
        CardonatorUser user = null;

        if (includes != null)
        {
            query = includes(query);
        }

        if (filter != null)
        {
            user = await query.FirstOrDefaultAsync(filter);
        }

        return user;
    }

    public Task<RepositoryResult> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RepositoryResult> UpdateAsync(CardonatorUser item)
    {
        throw new NotImplementedException();
    }

    public Task<RepositoryResult> SaveAsync()
    {
        return _saveContext.SaveAsync(_context);
    }
}
