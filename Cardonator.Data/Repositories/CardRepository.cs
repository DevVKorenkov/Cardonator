using Cardonator.Data.DataContext;
using Cardonator.Data.Repositories.Abstrations;
using Cardonator.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Cardonator.Data.Repositories;

public class CardRepository : ICardRepository
{
    private readonly CardsDataContext _context;
    private readonly DbSet<QuestionCard> _cards;
    private readonly ISaveContext _saveContext;

    public CardRepository(
        CardsDataContext cardsDataContext,
        ISaveContext saveContext)
    {
        _context = cardsDataContext;
        _cards = _context.Set<QuestionCard>();
        _saveContext = saveContext;
    }

    public async Task<RepositoryResult> AddAsync(QuestionCard item)
    {
        await _context.Cards.AddAsync(item);

        return await SaveAsync();
    }

    public async Task<IEnumerable<QuestionCard>> GetAllAsync(
        Expression<Func<QuestionCard, bool>> filter = null, 
        Func<IQueryable<QuestionCard>, IIncludableQueryable<QuestionCard, object>> includes = null)
    {
        IQueryable<QuestionCard> query = _cards;

        if(filter != null)
        {
            query = query.Where(filter);
        }

        if(includes != null)
        {
            query = includes(query);
        }

        return await query.ToListAsync();
    }

    public async Task<QuestionCard> GetAsync(
        Expression<Func<QuestionCard, bool>> filter = null, 
        Func<IQueryable<QuestionCard>, IIncludableQueryable<QuestionCard, object>> includes = null)
    {
        IQueryable<QuestionCard> query = _cards;
        QuestionCard card = null;

        if (includes != null)
        {
            query = includes(query);
        }

        if (filter != null)
        {
            card = await query.FirstOrDefaultAsync(filter);
        }

        return card;
    }

    public async Task<RepositoryResult> RemoveAsync(int id)
    {
        var card = await GetAsync(c => c.Id == id);

        _context.Cards.Remove(card);

        return await SaveAsync();
    }

    public async Task<RepositoryResult> UpdateAsync(QuestionCard item)
    {
        _context.Cards.Update(item);

        return await SaveAsync();
    }

    public async Task<RepositoryResult> SaveAsync()
    {
        return await _saveContext.SaveAsync(_context);
    }
}
