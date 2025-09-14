using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Application.Interfaces;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunoController(IAlunoService studentService)
        {
            _alunoService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromQuery] int numeroPagina = 1, int tamanhoPagina = 10)
        {
            var result = await _alunoService.GetAllPagedAsync(new PaginacaoRequest
            {
                NumeroPagina = numeroPagina,
                TamanhoPagina = tamanhoPagina
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlunoDTO dto)
        {
            var aluno = await _alunoService.AddAsync(dto);

            return Ok(aluno);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AlunoDTO dto)
        {
            var updated = await _alunoService.UpdateAsync(dto);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{alunoId:decimal}")]
        public async Task<IActionResult> Delete(decimal alunoId)
        {
            var success = await _alunoService.DeleteAsync(alunoId);
            if (!success) return NotFound();
            return Ok("Aluno removido com suucesso!");
        }
    }
}
