using Cardonator.Data.Repositories.Abstrations;
using Cardonator.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Cardonator.Data.Repositories;

public class SaveContext : ISaveContext
{
    public async Task<RepositoryResult> SaveAsync(DbContext dbContext)
    {
        RepositoryResult result = null;

        try
        {
            await dbContext.SaveChangesAsync();
            result = new RepositoryResult
            {
                IsSuccess = true,
            };
        }
        catch (Exception e)
        {
            result = new RepositoryResult
            {
                IsSuccess = false,
                Error = e.Message
            };
        }

        return result;
    }
}
