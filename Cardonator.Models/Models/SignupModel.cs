using System.ComponentModel.DataAnnotations;

namespace Cardonator.Models.Models;

public class SignupModel
{
    [Required]
    [MinLength(2, ErrorMessage = "The name doesn't match the lenght. Min is 2 symbols.")]
    [MaxLength(15, ErrorMessage = "The name doesn't match the lenght. Min is 15 symbols.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "password is required")]
    public string Password { get; set; }
}
