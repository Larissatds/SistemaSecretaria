using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Application.Interfaces;
using SistemaSecretaria.Domain.Entities;
using SistemaSecretaria.Domain.Interfaces;

namespace SistemaSecretaria.Application.Services
{
    public class TurmaService : ITurmaService
    {
        private readonly ITurmaRepository _repository;
        private readonly IMatriculaRepository _matriculaRepository;

        public TurmaService(ITurmaRepository repository, IMatriculaRepository matriculaRepository)
        {
            _repository = repository;
            _matriculaRepository = matriculaRepository;
        }

        public async Task<PaginacaoResult<TurmaDTO>> GetAllPagedAsync(PaginacaoRequest request)
        {
            var result = await _repository.GetAllPagedAsync(request);
            var matriculas = new List<Matricula>();

            foreach (var matricula in result.Items)
            {
                var matriculaResult = await _matriculaRepository.GetByTurmaAndAlunoAsync(matricula.IdTurma, null, new PaginacaoRequest { TamanhoPagina = int.MaxValue });
                matriculas.AddRange(matriculaResult.Items);
            }

            return new PaginacaoResult<TurmaDTO>
            {
                Items = result.Items.Select(x => new TurmaDTO
                {
                    IdTurma = x.IdTurma,
                    Nome = x.Nome,
                    Descricao = x.Descricao,
                    TotalAlunos = matriculas.Where(m => m.IdTurma == x.IdTurma).Count()
                }),
                TotalRegistros = result.TotalRegistros,
                NumeroPagina = result.NumeroPagina,
                TamanhoPagina = result.TamanhoPagina
            };
        }

        public async Task<TurmaDTO> AddAsync(TurmaDTO dto)
        {
            // Validações
            var valido = await this.Validations(dto, false);

            // Preenche o objeto Turma e registra na base de dados
            var turma = new Turma
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            await _repository.AddAsync(turma);

            dto.IdTurma = turma.IdTurma;

            return dto;
        }

        public async Task<TurmaDTO?> UpdateAsync(TurmaDTO dto)
        {
            // Validações
            var valido = await this.Validations(dto, true);

            // Preenche o objeto Turma e registra na base de dados
            valido.turma.Nome = dto.Nome;
            valido.turma.Descricao = dto.Descricao;

            await _repository.UpdateAsync(valido.turma);

            return dto;
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var turma = await _repository.GetByIdAsync(id);
            if (turma is null) return false;

            await _repository.DeleteAsync(turma);
            return true;
        }

        private async Task<(Turma turma, bool valido)> Validations(TurmaDTO dto, bool isUpdate)
        {
            if (dto == null)
                throw new ArgumentNullException("A entidade turma é inválida.");

            var turmaCadastrada = await _repository.GetByNameAsync(dto.Nome);

            if (turmaCadastrada != null || turmaCadastrada?.IdTurma > 0)
                throw new ArgumentException("Já existe uma turma cadastrada com o Nome informado.");

            if (isUpdate)
            {
                var turma = await _repository.GetByIdAsync(dto.IdTurma.Value);
                if (turma == null)
                    throw new ArgumentNullException("A entidade turma é inválida.");

                return (turma, true);
            }

            return (new Turma(), true);
        }
    }
}
