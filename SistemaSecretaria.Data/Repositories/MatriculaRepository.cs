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

        public async Task<PaginacaoResult<Matricula>> GetByTurmaAndAlunoAsync(decimal? turmaId, decimal? alunoId, PaginacaoRequest request)
        {
            var items = await _dbSet
                .Where(x => (turmaId == null ||x.IdTurma == turmaId) && (alunoId == null || x.IdAluno == alunoId))
                .Include(m => m.Turma)
                    .ThenInclude(t => t.Matriculas)
                    .ThenInclude(mt => mt.Aluno)
                .Include(m => m.Aluno)
                .Select(x => new Matricula
                {
                    IdMatricula = x.IdMatricula,
                    IdAluno = x.IdAluno,
                    IdTurma = x.IdTurma,
                    DataMatricula = x.DataMatricula,
                    Status = x.Status,
                    Aluno = new Aluno
                    {
                        IdAluno = x.Aluno.IdAluno,
                        NomeCompleto = x.Aluno.NomeCompleto
                    },
                    Turma = new Turma
                    {
                        IdTurma = x.Turma.IdTurma,
                        Nome = x.Turma.Nome,
                        Alunos = x.Turma.Matriculas
                        .Select(a => new Aluno
                        {
                            IdAluno = a.Aluno.IdAluno,
                            NomeCompleto = a.Aluno.NomeCompleto
                        })
                        .ToList()
                    }
                })
                .OrderBy(s => s.IdMatricula)
                .Skip((request.NumeroPagina - 1) * request.TamanhoPagina)
                .Take(request.TamanhoPagina)
                    .ToListAsync();

            return new PaginacaoResult<Matricula>
            {
                Items = items,
                TotalRegistros = items.Count,
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
