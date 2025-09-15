using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Application.Interfaces
{
    public interface IMatriculaService
    {
        Task<PaginacaoResult<MatriculaDTO>> GetAllPagedAsync(PaginacaoRequest request, decimal? idTurma);
        Task<MatriculaDTO> AddAsync(MatriculaDTO dto);
    }
}
