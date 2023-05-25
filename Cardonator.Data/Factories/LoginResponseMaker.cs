using System.Net;
using Cardonator.Data.Services.Abstractions;
using Cardonator.Models.Abstraction;
using Cardonator.Models.DTO;
using Cardonator.Models.Models;

namespace Cardonator.Data.Factories;

public class LoginResponseMaker : IResponse
{
    private readonly IUserService _userService;
    
    public LoginResponseMaker(IUserService userService)
    {
        _userService = userService;
    }
    
    public async Task<Response> MakeResponseAsync(
        string message, 
        string objName = null, 
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var authResponse = string.IsNullOrWhiteSpace(message)
            ? new AuthResponse(statusCode)
            : new AuthResponse(message, statusCode);

        if (objName != null)
        {
            authResponse.UserDTO = await _userService.GetAsync(objName);
        }

        return authResponse;
    }
}