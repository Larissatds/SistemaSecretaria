using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SistemaSecretaria.Data.Context;
using SistemaSecretaria.Domain.Entities;
using SistemaSecretaria.Domain.Interfaces;

namespace SistemaSecretaria.Data.Repositories
{
    public class MatriculaRepository : IMatriculaRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Matricula> _dbSet;

        public MatriculaRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Matricula>();
        }

        //public async Task<Matricula?> GetByIdAsync(decimal id) =>
        //    await _dbSet.FindAsync(id);

        public async Task<PaginacaoResult<Matricula>> GetByTurmaAsync(decimal turmaId, PaginacaoRequest request)
        {
            var totalCount = await _dbSet.CountAsync();
            var items = await _dbSet
                .Where(x => x.IdTurma == turmaId)
                .OrderBy(s => s.IdMatricula)
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                    .ToListAsync();

            return new PaginacaoResult<Matricula>
            {
                Items = items,
                TotalRegistros = totalCount,
                NumeroPagina = request.NumeroPagina,
                TamanhoPagina = request.TamanhoPagina
            };

        }

        public async Task<PaginacaoResult<Matricula>> GetAllPagedAsync(PaginacaoRequest request)
        {
            var totalCount = await _dbSet.CountAsync();
            var items = await _dbSet
                .OrderBy(s => s.IdMatricula)
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                .ToListAsync();

            return new PaginacaoResult<Matricula>
            {
                Items = items,
                TotalRegistros = totalCount,
                NumeroPagina = request.NumeroPagina,
                TamanhoPagina = request.TamanhoPagina
            };
        }

        public async Task AddAsync(Matricula entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
