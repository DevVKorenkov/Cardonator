using Cardonator.Models.Abstraction;
using Cardonator.Models.Models;

namespace Cardonator.Data.Services.Abstractions;

public interface IAuthService
{
    Task<AuthResponse?> Signup(SignupModel signupModel);
    Task<AuthResponse?> Login(LoginModel loginModel);
    Task<Response?> Logout();
}
