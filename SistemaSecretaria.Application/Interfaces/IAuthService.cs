using SistemaSecretaria.Application.DTOs;

namespace SistemaSecretaria.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
    }
}
