using Cardonator.Models.Abstraction;
using System.Net;

namespace Cardonator.Models.Models;

public class ActionResponce : Response
{
    public ActionResponce(string message, HttpStatusCode statusCode) : base(message, statusCode)
    {
        
    }

    public ActionResponce (HttpStatusCode statusCode) : base(statusCode) 
    {
        
    }
}
