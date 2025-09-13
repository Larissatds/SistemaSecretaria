namespace SistemaSecretaria.Application.DTOs
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = null!;
        public DateTime DataExpiracao { get; set; }
    }
}
