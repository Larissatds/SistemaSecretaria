using System.Net.Mail;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Application.Interfaces;
using SistemaSecretaria.Data.Repositories;
using SistemaSecretaria.Domain.Entities;
using SistemaSecretaria.Domain.Interfaces;

namespace SistemaSecretaria.Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly IAlunoRepository _repository;
        private readonly PasswordHasher<Aluno> _hasher;


        public AlunoService(IAlunoRepository repository)
        {
            _repository = repository;
            _hasher = new PasswordHasher<Aluno>();
        }

        public async Task<PaginacaoResult<AlunoDTO>> GetAllPagedAsync(PaginacaoRequest request)
        {
            var result = await _repository.GetAllPagedAsync(request);

            return new PaginacaoResult<AlunoDTO>
            {
                Items = result.Items.Select(x => new AlunoDTO
                {
                    IdAluno = x.IdAluno,
                    NomeCompleto = x.NomeCompleto,
                    CPF = x.CPF,
                    DataNascimento = x.DataNascimento,
                    Email = x.Email,
                    SenhaHash = x.SenhaHash
                }),
                TotalRegistros = result.TotalRegistros,
                NumeroPagina = result.NumeroPagina,
                TamanhoPagina = result.TamanhoPagina
            };
        }

        public async Task<AlunoDTO> AddAsync(AlunoDTO dto)
        {
            // Validações
            var valido = await this.Validations(dto, false);

            // Hash da senha
            var senha = _hasher.HashPassword(null, dto.SenhaHash);

            // Preenche o objeto Aluno e registra na base de dados
            var aluno = new Aluno
            {
                NomeCompleto = dto.NomeCompleto,
                CPF = valido.cpf,
                DataNascimento = dto.DataNascimento,
                Email = dto.Email,
                SenhaHash = senha
            };

            await _repository.AddAsync(aluno);

            dto.IdAluno = aluno.IdAluno;
            dto.SenhaHash = senha;

            return dto;
        }

        public async Task<AlunoDTO?> UpdateAsync(AlunoDTO dto)
        {
            // Validações
            var valido = await this.Validations(dto, true);

            // Hash da senha
            var senha = _hasher.HashPassword(null, dto.SenhaHash);

            // Preenche o objeto Aluno e registra na base de dados
            valido.aluno.NomeCompleto = dto.NomeCompleto;
            valido.aluno.CPF = valido.cpf;
            valido.aluno.DataNascimento = dto.DataNascimento;
            valido.aluno.Email = dto.Email;
            valido.aluno.SenhaHash = senha;

            dto.SenhaHash = senha;

            await _repository.UpdateAsync(valido.aluno);

            return dto;
        }

        public async Task<bool> DeleteAsync(decimal id)
        {
            var aluno = await _repository.GetByIdAsync(id);
            if (aluno is null) return false;

            await _repository.DeleteAsync(aluno);
            return true;
        }

        private async Task<(string cpf, bool valido, Aluno aluno)> Validations(AlunoDTO dto, bool isUpdate)
        {
            if (dto == null)
                throw new ArgumentNullException("A entidade aluno é inválida.");
            if (string.IsNullOrWhiteSpace(dto.CPF))
                throw new ArgumentException("O CPF informado é inválido.");

            var alunoCadastrado = await _repository.GetByCPFOrEmailAsync(dto.CPF, dto.CPF);

            if (alunoCadastrado != null || alunoCadastrado?.IdAluno > 0)
                throw new ArgumentException("Já existe um aluno cadastrado com o CPF ou Email informado.");

            // Remove caracteres não numéricos do CPF
            var cpf = Regex.Replace(dto.CPF, "[^0-9]", "");

            if (cpf.Length != 11)
                throw new ArgumentException("O CPF informado é inválido.");

            // Validações de CPF e Email
            var cpfValido = this.ValidateCPF(cpf);
            if (!cpfValido)
                throw new ArgumentException("O CPF informado é inválido.");

            var emailValido = this.ValidateEmail(dto.Email);
            if (!emailValido)
                throw new ArgumentException("O email informado é inválido.");

            if (isUpdate)
            {
                var aluno = await _repository.GetByIdAsync(dto.IdAluno.Value);
                if (aluno == null)
                    throw new ArgumentNullException("A entidade aluno é inválida.");

                return (cpf, true, aluno);
            }

            return (cpf, true, new Aluno());
        }

        private bool ValidateCPF(string cpf)
        {
            try
            {    
                // Verifica CPFs inválidos
                if (cpf.All(c => c == cpf[0]))
                    return false;

                // Calculo dos dígitos verificadores
                int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCpf = cpf.Substring(0, 9);
                int soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                int resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;
                string digito = resto.ToString();

                tempCpf += digito;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;
                resto = resto < 2 ? 0 : 11 - resto;
                digito += resto.ToString();

                return cpf.EndsWith(digito);
            }
            catch
            {
                return false;
            }
        }

        private bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[a-zA-Z]{2,}$",
                    RegexOptions.Compiled | RegexOptions.IgnoreCase);

                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
    }
}
