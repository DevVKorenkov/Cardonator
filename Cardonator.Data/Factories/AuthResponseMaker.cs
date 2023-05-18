using Cardonator.Data.Services.Abstractions;
using Cardonator.Models.Abstraction;
using Cardonator.Models.DTO;
using Cardonator.Models.Models;
using System.Net;

namespace Cardonator.Data.Factories;

public class AuthResponseMaker : IResponse
{
    private readonly IUserService _userService;
    public AuthResponseMaker(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<Response> MakeResponceAsync( 
        string message,
        string objNname = null,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        //var userDTO = await _userService.GetAsync(objNname);

        var authResponse = string.IsNullOrWhiteSpace(message) 
            ? new AuthResponse(statusCode)
            : new AuthResponse(message, statusCode);

        //authResponse.UserDTO = userDTO;

        return authResponse;
    }
}
