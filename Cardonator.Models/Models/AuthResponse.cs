using Cardonator.Models.Abstraction;
using Cardonator.Models.DTO;
using System.Net;

namespace Cardonator.Models.Models;

public class AuthResponse : Response
{
    public UserDTO UserDTO { get; set; }

    public AuthResponse(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {

    }

    public AuthResponse(HttpStatusCode statusCode) : base(statusCode)
    {

    }
}
