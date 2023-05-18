using System.Net;

namespace Cardonator.Models.Abstraction;

public interface IResponse
{
    Task<Response> MakeResponceAsync(
        string message,
        string objNname = null, 
        HttpStatusCode statusCode = HttpStatusCode.OK);
}
