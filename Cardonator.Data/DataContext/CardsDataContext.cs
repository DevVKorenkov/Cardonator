using Cardonator.Models.Abstraction;
using Cardonator.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cardonator.Data.DataContext;

public class CardsDataContext : IdentityDbContext<IdentityUser>
{
    public DbSet<CardonatorUser> CardonatorUsers { get; set; }
    public DbSet<CardCollection> CardCollections { get; set; }
    public DbSet<QuestionCard> Cards { get; set; }

    public CardsDataContext(DbContextOptions<CardsDataContext> options) : base(options)
    {
        
    }
}
