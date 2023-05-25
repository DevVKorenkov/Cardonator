using Cardonator.Data.Services.Abstractions;
using Cardonator.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cardonator.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("signup")]
    public async Task<IActionResult> Signup(SignupModel signupModel)
    {
        var result = await _authService.Signup(signupModel);

        return Json(result);
    }

    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> Signin(LoginModel loginModel)
    {
        var result = await _authService.Login(loginModel);
        
        return Json(result);
    }
}
