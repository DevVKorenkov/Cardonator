using Cardonator.Models.Models;

namespace Cardonator.Models.DTO;

public record UserDTO(string Id, string Name, string Email, IEnumerable<CardCollection> CardCollections);
