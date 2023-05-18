using System.ComponentModel.DataAnnotations;

namespace Cardonator.Models.Abstraction;

public abstract class Card
{
    [Key]
    public int Id { get; set; }
    [MaxLength(length: 15)]
    public string Name { get; set; }
    [MaxLength(length: 512)]
    public string FrontSide { get; set; }
    [MaxLength(length: 1024)]
    public string BackSide { get; set; }
}
