using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Annotations;
using AdOptimize.Models.DTOs;
using AdOptimize.Services;

namespace AdOptimize.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioService _anuncioService;

        public AnuncioController(IAnuncioService anuncioService)
        {
            _anuncioService = anuncioService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pega todos os anúncios", Description = "Use este endpoint para puxar os valores de todos os anúncios do sistema.")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var anuncios = await _anuncioService.GetAllAnunciosAsync();
            return Ok(anuncios);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Pega o valor de um anúncio existente", Description = "Use este endpoint para puxar os dados de um anúncio do sistema.")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var anuncio = await _anuncioService.GetAnuncioByIdAsync(id);
            if (anuncio == null)
            {
                return NotFound($"Anúncio com ID '{id}' não encontrado.");
            }
            return Ok(anuncio);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo anúncio", Description = "Use este endpoint para adicionar um novo anúncio ao sistema.")]
        public async Task<IActionResult> Create([FromBody] AnuncioDTO anuncioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdAnuncio = await _anuncioService.CreateAnuncioAsync(anuncioDTO);

                if (createdAnuncio == null)
                {
                    return BadRequest("Erro ao criar o anúncio.");
                }

                return CreatedAtAction(nameof(GetById), new { id = createdAnuncio.Id }, createdAnuncio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Captura exceções e retorna uma mensagem de erro
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Altera um anúncio existente", Description = "Use este endpoint para alterar um anúncio do sistema.")]
        public async Task<IActionResult> Update(int id, [FromBody] AnuncioDTO anuncioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _anuncioService.UpdateAnuncioAsync(id, anuncioDTO);
            if (result == null)
            {
                return NotFound($"Anúncio com ID '{id}' não encontrado.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um anúncio existente", Description = "Use este endpoint para deletar um anúncio do sistema.")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna os erros de validação
            }

            var result = await _anuncioService.DeleteAnuncioAsync(id);
            if (!result)
            {
                return NotFound($"Anúncio com ID '{id}' não encontrado.");
            }
            return NoContent();
        }
    }
}
