using AdOptimize.Models.Models;
using AdOptimize.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {
        private readonly IAnuncioService _anuncioService;

        public AnuncioController(IAnuncioService anuncioService)
        {
            _anuncioService = anuncioService;
        }

        // GET: api/Anuncio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Anuncio>>> GetAnuncios()
        {
            var anuncios = await _anuncioService.GetAllAnunciosAsync();
            return Ok(anuncios);
        }

        // GET: api/Anuncio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Anuncio>> GetAnuncio(int id)
        {
            var anuncio = await _anuncioService.GetAnuncioByIdAsync(id);

            if (anuncio == null)
            {
                return NotFound();
            }

            return Ok(anuncio);
        }

        // POST: api/Anuncio
        [HttpPost]
        public async Task<ActionResult<Anuncio>> CreateAnuncio(Anuncio anuncio)
        {
            if (anuncio == null)
            {
                return BadRequest();
            }

            var createdAnuncio = await _anuncioService.CreateAnuncioAsync(anuncio);
            return CreatedAtAction(nameof(GetAnuncio), new { id = createdAnuncio.Id }, createdAnuncio);
        }

        // PUT: api/Anuncio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnuncio(int id, Anuncio anuncio)
        {
            if (id != anuncio.Id)
            {
                return BadRequest();
            }

            var updatedAnuncio = await _anuncioService.UpdateAnuncioAsync(anuncio);

            if (updatedAnuncio == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Anuncio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnuncio(int id)
        {
            var anuncio = await _anuncioService.GetAnuncioByIdAsync(id);
            if (anuncio == null)
            {
                return NotFound();
            }

            await _anuncioService.DeleteAnuncioAsync(id);
            return NoContent();
        }
    }
}
