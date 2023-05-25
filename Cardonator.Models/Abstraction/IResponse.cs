using System.Net;

namespace Cardonator.Models.Abstraction;

public interface IResponse
{
    Task<Response> MakeResponseAsync(
        string message,
        string objNname = null, 
        HttpStatusCode statusCode = HttpStatusCode.OK);
}
