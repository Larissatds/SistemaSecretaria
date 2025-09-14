using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.Interfaces
{
    public interface ITurmaService
    {
        Task<PaginacaoResult<TurmaDTO>> GetAllPagedAsync(PaginacaoRequest request);
        Task<TurmaDTO> AddAsync(TurmaDTO dto);
        Task<TurmaDTO?> UpdateAsync(TurmaDTO dto);
        Task<bool> DeleteAsync(decimal id);
    }
}
