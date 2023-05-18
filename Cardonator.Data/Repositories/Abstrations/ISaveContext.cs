using Cardonator.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Cardonator.Data.Repositories.Abstrations;

public interface ISaveContext
{
    Task<RepositoryResult> SaveAsync(DbContext dbContext);
}
