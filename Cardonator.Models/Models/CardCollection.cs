using Cardonator.Models.Abstraction;
using System.ComponentModel.DataAnnotations;

namespace Cardonator.Models.Models;

public class CardCollection
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Name { get; set; }
    [MaxLength(512)]
    public string Description { get; set; }
    public ICollection<Card> Cards { get; set; }
}
