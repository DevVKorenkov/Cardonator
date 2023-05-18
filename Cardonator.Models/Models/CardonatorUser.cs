using Microsoft.AspNetCore.Identity;

namespace Cardonator.Models.Models;

public class CardonatorUser : IdentityUser
{
    public ICollection<CardCollection> CardCollections { get; set; }
}
