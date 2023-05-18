using Cardonator.Data.Repositories.Abstrations;
using Cardonator.Data.Services.Abstractions;
using Cardonator.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Cardonator.Data.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// objName can be userId because this field is string in AspNet IdentityUser
    /// </summary>
    /// <param name="objName"></param>
    /// <returns></returns>
    public async Task<UserDTO> GetAsync(string objName)
    {
        var user = await _userRepository.GetAsync(
            u => u.Id == objName 
            || u.UserName == objName 
            || u.Email == objName,
            includes: i => i.Include(u => u.CardCollections)
            .ThenInclude(c => c.Cards));

        var userDTO = new UserDTO(user.Id, user.UserName, user.Email, user.CardCollections);

        return userDTO;
    }
}
