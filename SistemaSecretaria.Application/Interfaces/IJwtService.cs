using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.Interfaces
{
    public interface IJwtService
    {
        (string token, DateTime expiresAt) GenerateToken(Usuario usuario);
    }
}
