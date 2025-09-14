using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Domain.Interfaces
{
    public interface ITurmaRepository
    {
        Task<Turma?> GetByIdAsync(decimal id);
        Task<Turma?> GetByNameAsync(string nome);
        Task<PaginacaoResult<Turma>> GetAllPagedAsync(PaginacaoRequest request);
        Task AddAsync(Turma entity);
        Task UpdateAsync(Turma entity);
        Task DeleteAsync(Turma entity);
    }
}
