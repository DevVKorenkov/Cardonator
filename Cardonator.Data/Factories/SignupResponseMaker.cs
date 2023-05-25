using Cardonator.Models.Abstraction;
using Cardonator.Models.Models;
using System.Net;

namespace Cardonator.Data.Factories;

public class SignupResponseMaker : IResponse
{
    public SignupResponseMaker()
    {
        
    }

    public async Task<Response> MakeResponseAsync( 
        string message,
        string objName = null,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var authResponse = string.IsNullOrWhiteSpace(message) 
            ? new AuthResponse(statusCode)
            : new AuthResponse(message, statusCode);

        return authResponse;
    }
}
