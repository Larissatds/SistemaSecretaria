using Microsoft.EntityFrameworkCore;
using SistemaSecretaria.Data.Context;
using SistemaSecretaria.Domain.Entities;
using SistemaSecretaria.Domain.Interfaces;

namespace SistemaSecretaria.Data.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Turma> _dbSet;

        public TurmaRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Turma>();
        }

        public async Task<Turma?> GetByIdAsync(decimal id) =>
            await _dbSet.FindAsync(id);

        public async Task<Turma?> GetByNameAsync(string nome) =>
         await _dbSet.FirstOrDefaultAsync(x => x.Nome.Equals(nome));

        public async Task<PaginacaoResult<Turma>> GetAllPagedAsync(PaginacaoRequest request)
        {
            var totalCount = await _dbSet.CountAsync();
            var items = await _dbSet
                .OrderBy(s => s.Nome)
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            return new PaginacaoResult<Turma>
            {
                Items = items,
                TotalRegistros = totalCount,
                NumeroPagina = request.NumeroPagina,
                TamanhoPagina = request.TamanhoPagina
            };
        }

        public async Task AddAsync(Turma entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Turma entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Turma entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
