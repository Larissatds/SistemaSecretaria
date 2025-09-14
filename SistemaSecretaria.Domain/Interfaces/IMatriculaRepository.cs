using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<PaginacaoResult<Matricula>> GetByTurmaAsync(decimal turmaId, PaginacaoRequest request);
        Task<PaginacaoResult<Matricula>> GetAllPagedAsync(PaginacaoRequest request);
        Task AddAsync(Matricula entity);
    }
}
