using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Application.Interfaces;
using SistemaSecretaria.Data.Context;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtService _jwt;
        private readonly PasswordHasher<Usuario> _hasher;

        public AuthService(AppDbContext db, JwtService jwt)
        {
            _db = db;
            _jwt = jwt;
            _hasher = new PasswordHasher<Usuario>();
        }

        public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _db.Usuarios.Include(u => u.TipoUsuario)
                                      .SingleOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Credenciais inválidas, tente novamente.");

            var result = _hasher.VerifyHashedPassword(user, user.SenhaHash, dto.Senha);
            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Credenciais inválidas, tente novamente.");

            user.DataUltimoLogin = DateTime.UtcNow;
            await _db.SaveChangesAsync();

            var (token, expires) = _jwt.GenerateToken(user);
            return new AuthResponseDTO { Token = token, DataExpiracao = expires };
        }
    }

}
