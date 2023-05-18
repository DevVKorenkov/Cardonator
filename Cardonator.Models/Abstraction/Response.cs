using System.Net;

namespace Cardonator.Models.Abstraction;

public abstract class Response
{
    private ICollection<string> _messages;
    public ICollection<string> Messages => _messages;
    public HttpStatusCode StatusCode { get; set; }

    public Response(string message, HttpStatusCode statusCode)
    {
        _messages = new List<string>() { message };
        StatusCode = statusCode;
    }

    public Response(HttpStatusCode statusCode)
    {
        _messages = new List<string>();
        StatusCode = statusCode;
    }

    public virtual Response AddToMessage(string addition)
    {
        _messages.Add(addition);
        return this;
    }
}
