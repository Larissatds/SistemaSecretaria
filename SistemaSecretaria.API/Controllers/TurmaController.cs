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
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaService _turmaService;

        public TurmaController(ITurmaService turmaService)
        {
            _turmaService = turmaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromQuery] int numeroPagina = 1, int tamanhoPagina = 10)
        {
            var result = await _turmaService.GetAllPagedAsync(new PaginacaoRequest
            {
                NumeroPagina = numeroPagina,
                TamanhoPagina = tamanhoPagina
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TurmaDTO dto)
        {
            var turma = await _turmaService.AddAsync(dto);

            return Ok(turma);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TurmaDTO dto)
        {
            var updated = await _turmaService.UpdateAsync(dto);
            if (updated is null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{turmaId:decimal}")]
        public async Task<IActionResult> Delete(decimal turmaId)
        {
            var success = await _turmaService.DeleteAsync(turmaId);
            if (!success) return NotFound();
            return Ok("Turma removida com sucesso!");
        }
    }
}
