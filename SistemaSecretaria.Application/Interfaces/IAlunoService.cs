using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.Interfaces
{
    public interface IAlunoService
    {
        Task<PaginacaoResult<AlunoDTO>> GetAllPagedAsync(PaginacaoRequest request, string? nome);
        Task<AlunoDTO> AddAsync(AlunoDTO dto);
        Task<AlunoDTO?> UpdateAsync(AlunoDTO dto);
        Task<bool> DeleteAsync(decimal id);
    }
}
