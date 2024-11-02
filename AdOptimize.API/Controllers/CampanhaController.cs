using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using AdOptimize.Models.DTOs;
using AdOptimize.Services;

namespace AdOptimize.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampanhaController : ControllerBase
    {
        private readonly ICampanhaService _campanhaService;

        public CampanhaController(ICampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pega todas as campanhas", Description = "Use este endpoint para puxar os valores de todas as campanhas do sistema.")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var campanhas = await _campanhaService.GetAllCampanhasAsync();
            return Ok(campanhas);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Pega o valor de uma campanha existente", Description = "Use este endpoint para puxar os dados de uma campanha do sistema.")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var campanha = await _campanhaService.GetCampanhaByIdAsync(id);
            if (campanha == null)
            {
                return NotFound($"Campanha com ID '{id}' não encontrada.");
            }
            return Ok(campanha);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova campanha", Description = "Use este endpoint para adicionar uma nova campanha ao sistema.")]
        public async Task<IActionResult> Create([FromBody] CampanhaDTO campanhaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdCampanha = await _campanhaService.CreateCampanhaAsync(campanhaDTO);

                if (createdCampanha == null)
                {
                    return BadRequest("Erro ao criar a campanha.");
                }

                return CreatedAtAction(nameof(GetById), new { id = createdCampanha.Id }, createdCampanha);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Captura exceções e retorna uma mensagem de erro
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Altera uma campanha existente", Description = "Use este endpoint para alterar uma campanha do sistema.")]
        public async Task<IActionResult> Update(int id, [FromBody] CampanhaDTO campanhaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _campanhaService.UpdateCampanhaAsync(id, campanhaDTO);
            if (result == null)
            {
                return NotFound($"Campanha com ID '{id}' não encontrada.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta uma campanha existente", Description = "Use este endpoint para deletar uma campanha do sistema.")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _campanhaService.DeleteCampanhaAsync(id);
            if (!result)
            {
                return NotFound($"Campanha com ID '{id}' não encontrada.");
            }
            return NoContent();
        }
    }
}
