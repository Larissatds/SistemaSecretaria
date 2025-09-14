using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Domain.Interfaces
{
    public interface IAlunoRepository
    {
        Task<Aluno?> GetByIdAsync(decimal id);
        Task<Aluno?> GetByCPFOrEmailAsync(string cpf, string email);
        Task<PaginacaoResult<Aluno>> GetAllPagedAsync(PaginacaoRequest request);
        Task AddAsync(Aluno entity);
        Task UpdateAsync(Aluno entity);
        Task DeleteAsync(Aluno entity);
    }
}
