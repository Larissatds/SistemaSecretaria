using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaSecretaria.Application.DTOs;
using SistemaSecretaria.Application.Interfaces;
using SistemaSecretaria.Application.Services;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminOnly")]
    public class MatriculaController : ControllerBase
    {
        private readonly IMatriculaService _matriculaService;

        public MatriculaController(IMatriculaService matriculaService)
        {
            _matriculaService = matriculaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaged([FromBody] MatriculaRequest matricula)
        {
            var result = await _matriculaService.GetAllPagedAsync(new PaginacaoRequest
            {
                NumeroPagina = matricula.NumeroPagina,
                TamanhoPagina = matricula.TamanhoPagina
            }, matricula.IdTurma);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatriculaDTO dto)
        {
            var matricula = await _matriculaService.AddAsync(dto);

            return Ok(matricula);
        }
    }
}
