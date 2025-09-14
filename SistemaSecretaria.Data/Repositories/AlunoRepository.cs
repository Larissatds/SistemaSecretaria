using Microsoft.EntityFrameworkCore;
using SistemaSecretaria.Data.Context;
using SistemaSecretaria.Domain.Entities;
using SistemaSecretaria.Domain.Interfaces;

namespace SistemaSecretaria.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Aluno> _dbSet;

        public AlunoRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Aluno>();
        }

        public async Task<Aluno?> GetByIdAsync(decimal id) =>
            await _dbSet.FindAsync(id);

        public async Task<Aluno?> GetByCPFOrEmailAsync(string cpf, string email) =>
            await _dbSet.FirstOrDefaultAsync(x => x.CPF.Equals(cpf) || x.Email.Equals(email));

        public async Task<PaginacaoResult<Aluno>> GetAllPagedAsync(PaginacaoRequest request, string? nome)
        {
            var items = await _dbSet
                .Where(a => string.IsNullOrEmpty(nome) || a.NomeCompleto.Contains(nome))
                .OrderBy(s => s.NomeCompleto)
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            return new PaginacaoResult<Aluno>
            {
                Items = items,
                TotalRegistros = items.Count(),
                NumeroPagina = request.NumeroPagina,
                TamanhoPagina = request.TamanhoPagina
            };
        }

        public async Task AddAsync(Aluno entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Aluno entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Aluno entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
