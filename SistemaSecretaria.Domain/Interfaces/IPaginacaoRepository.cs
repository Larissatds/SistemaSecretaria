using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Domain.Interfaces
{
    public interface IPaginacaoRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PaginacaoResult<T>> GetPagedAsync(PaginacaoRequest request);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
