using Cardonator.Data.DataContext;
using Cardonator.Data.Repositories.Abstrations;
using Cardonator.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cardonator.Data.Repositories;

public class CardCollectionsRepository : ICardCollectionsRepository
{
    private readonly CardsDataContext _context;
    private readonly DbSet<CardCollection> _cardCollections;
    private readonly ISaveContext _saveContext;
    public CardCollectionsRepository(
        CardsDataContext cardsDataContext, 
        ISaveContext saveContext)
    {
        _context = cardsDataContext;
        _cardCollections = _context.Set<CardCollection>();
        _saveContext = saveContext;
    }

    public async Task<RepositoryResult> AddAsync(CardCollection item)
    {
        await _context.CardCollections.AddAsync(item);

        return await SaveAsync();
    }

    public async Task<IEnumerable<CardCollection>> GetAllAsync(
        Expression<Func<CardCollection, bool>> filter = null, 
        Func<IQueryable<CardCollection>, IIncludableQueryable<CardCollection, object>> includes = null)
    {
        IQueryable<CardCollection> query = _cardCollections;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            query = includes(query);
        }

        return await query.ToListAsync();
    }

    public async Task<CardCollection> GetAsync(
        Expression<Func<CardCollection, bool>> filter = null,
        Func<IQueryable<CardCollection>, IIncludableQueryable<CardCollection, object>> includes = null)
    {
        IQueryable<CardCollection> query = _cardCollections;
        CardCollection cardCollection = null;

        if (includes != null)
        {
            query = includes(query);
        }

        if (filter != null)
        {
            cardCollection = await query.FirstOrDefaultAsync(filter);
        }

        return cardCollection;
    }

    public async Task<RepositoryResult> RemoveAsync(int id)
    {
        var collection = await GetAsync(c => c.Id == id);

        _context.CardCollections.Remove(collection);

        return await SaveAsync();
    }

    public async Task<RepositoryResult> UpdateAsync(CardCollection item)
    {
        _context.Update(item);

        return await SaveAsync();
    }

    public async Task<RepositoryResult> SaveAsync()
    {
        return await _saveContext.SaveAsync(_context);
    }
}
