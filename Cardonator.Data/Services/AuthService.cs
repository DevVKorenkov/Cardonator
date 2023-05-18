using Cardonator.Data.Factories;
using Cardonator.Data.Repositories.Abstrations;
using Cardonator.Data.Services.Abstractions;
using Cardonator.Models.Abstraction;
using Cardonator.Models.DTO;
using Cardonator.Models.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net;
using System.Security.Claims;

namespace Cardonator.Data.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<CardonatorUser> _userManager;
    private readonly SignInManager<CardonatorUser> _signInManager;
    private readonly AuthResponseMaker _authResponseMaker;
    private readonly IUserRepository _userRepository;

    public AuthService(
        UserManager<CardonatorUser> userManager,
        SignInManager<CardonatorUser> signInManager,
        IUserRepository userRepository,
        IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userRepository = userRepository;
        _authResponseMaker = new AuthResponseMaker(userService);
    }

    public async Task<AuthResponse?> Login(LoginModel loginModel)
    {
        Response authResponse = null;

        var user = await _userManager.FindByNameAsync(loginModel.Login) ?? await _userManager.FindByEmailAsync(loginModel.Login);

        if(user is null)
        {
            authResponse = await _authResponseMaker.MakeResponceAsync(
                $"There isn't user with name or email {loginModel.Login}",
                loginModel.Login,  
                HttpStatusCode.NotFound);
        }
        else
        {
            var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);

            if (result.Succeeded)
            {
                authResponse = await _authResponseMaker.MakeResponceAsync(
                    user.UserName,
                    "You are succsessfully logined",
                    HttpStatusCode.OK);
            }
            else
            {
                authResponse = await _authResponseMaker.MakeResponceAsync(
                    message: "Wrong password",
                    statusCode: HttpStatusCode.Unauthorized);
            }
        }

        return authResponse as AuthResponse;
    }

    public async Task<AuthResponse> Signup(SignupModel signupModel)
    {
        var checkUser = await _userManager.FindByNameAsync(signupModel.Name) ?? await _userManager.FindByEmailAsync(signupModel.Email);
        bool isAllowedToSignup;
        AuthResponse authResponse = (AuthResponse)await _authResponseMaker.MakeResponceAsync(string.Empty, signupModel.Name, HttpStatusCode.BadRequest);

        if (checkUser != null && checkUser.UserName == signupModel.Name)
        {
            authResponse.AddToMessage($"There is another user with name {signupModel.Name}");
        }

        if (checkUser != null && checkUser.Email == signupModel.Email)
        {
            authResponse.AddToMessage($"There is another user with email {signupModel.Email}");
        }

        isAllowedToSignup = checkUser == null;

        if (isAllowedToSignup)
        {
            var user = new CardonatorUser
            {
                UserName = signupModel.Name,
                Email = signupModel.Email,
            };

            var result = await _userManager.CreateAsync(user, signupModel.Password);

            user = await _userRepository.GetAsync(u => u.UserName == user.UserName);

            if (result.Succeeded) 
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                await _userManager.AddClaimsAsync(user, claims);

                await Login(new LoginModel { Login = user.UserName, Password = signupModel.Password});

                authResponse.AddToMessage("A user has been successfully created.");
                authResponse.UserDTO = new UserDTO(user.Id, user.UserName, user.Email, user.CardCollections);
                authResponse.StatusCode = HttpStatusCode.OK;
            }
        }

        return authResponse;
    }

    public async Task<Response> Logout()
    {
        Response authResponse = await _authResponseMaker.MakeResponceAsync(message: string.Empty);

        try
        {
            await _signInManager.SignOutAsync();
            authResponse.AddToMessage("You have successfully signed out.");
            authResponse.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            authResponse.AddToMessage($"Something wrong. {ex.Message}");
            authResponse.StatusCode = HttpStatusCode.BadRequest;
        }

        return authResponse;
    }


}
