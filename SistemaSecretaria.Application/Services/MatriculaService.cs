using Azure.Core;
using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Application.Interfaces;
using SistemaSecretaria.Domain.Entities;
using SistemaSecretaria.Domain.Interfaces;

namespace SistemaSecretaria.Application.Services
{
    public class MatriculaService : IMatriculaService
    {
        private readonly IMatriculaRepository _repository;
        private readonly IAlunoRepository _alunoRepository;
        private readonly ITurmaRepository _TurmaRrepository;

        public MatriculaService(IMatriculaRepository repository, IAlunoRepository alunoRepository, ITurmaRepository turmaRepository)
        {
            _repository = repository;
            _alunoRepository = alunoRepository;
            _TurmaRrepository = turmaRepository;
        }

        public async Task<PaginacaoResult<MatriculaDTO>> GetAllPagedAsync(PaginacaoRequest request, decimal? idTurma)
        {
            var result = await _repository.GetByTurmaAndAlunoAsync(idTurma, null, request);

            return new PaginacaoResult<MatriculaDTO>
            {
                Items = result.Items.Select(x => new MatriculaDTO
                {
                    IdMatricula = x.IdMatricula,
                    IdAluno = x.IdAluno,
                    IdTurma = x.IdTurma,
                    DataMatricula = x.DataMatricula,
                    Status = x.Status,
                    Turma = x.Turma,
                    Aluno = x.Aluno
                }),
                TotalRegistros = result.TotalRegistros,
                NumeroPagina = result.NumeroPagina,
                TamanhoPagina = result.TamanhoPagina
            };
        }

        public async Task<MatriculaDTO> AddAsync(MatriculaDTO dto)
        {
            // Validações
            var valido = await this.Validations(dto);

            // Preenche o objeto Matricula e registra na base de dados
            var matricula = new Matricula
            {
                IdAluno = dto.IdAluno,
                IdTurma = dto.IdTurma,
                DataMatricula = DateTime.Now,
                Status = dto.Status
            };

            await _repository.AddAsync(matricula);

            dto.IdTurma = matricula.IdTurma;

            return dto;
        }

        private async Task<(Matricula matricula, bool valido)> Validations(MatriculaDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException("A entidade matricula é inválida.");

            var matriculas = await _repository.GetByTurmaAndAlunoAsync(dto.IdTurma, dto.IdAluno, new PaginacaoRequest { TamanhoPagina = int.MaxValue });
            if (matriculas?.Items?.Count() > 0)
                throw new ArgumentException("O aluno informado já está matriculado nessa turma.");

            var aluno = await _alunoRepository.GetByIdAsync(dto.IdAluno);
            if (aluno == null)
                throw new ArgumentException("O aluno informado não foi encontrado.");

            var turma = await _TurmaRrepository.GetByIdAsync(dto.IdTurma);
            if (turma == null)
                throw new ArgumentException("A turma informada não foi encontrada.");

            return (new Matricula(), true);
        }
    }
}
