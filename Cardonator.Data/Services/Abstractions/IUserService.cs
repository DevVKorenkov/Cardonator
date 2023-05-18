using Cardonator.Models.DTO;

namespace Cardonator.Data.Services.Abstractions;

public interface IUserService
{
    Task<UserDTO> GetAsync(string objName);
}
