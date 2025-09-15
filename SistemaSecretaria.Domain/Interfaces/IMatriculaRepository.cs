using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Domain.Interfaces
{
    public interface IMatriculaRepository
    {
        Task<PaginacaoResult<Matricula>> GetByTurmaAndAlunoAsync(decimal? turmaId, decimal? alunoId, PaginacaoRequest request);
        Task AddAsync(Matricula entity);
    }
}
