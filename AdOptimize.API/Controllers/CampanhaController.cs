using AdOptimize.Models.Models;
using AdOptimize.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampanhaController : ControllerBase
    {
        private readonly ICampanhaService _campanhaService;

        public CampanhaController(ICampanhaService campanhaService)
        {
            _campanhaService = campanhaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Campanha>>> GetAllCampanhas()
        {
            var campanhas = await _campanhaService.GetAllCampanhasAsync();
            return Ok(campanhas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Campanha>> GetCampanhaById(int id)
        {
            var campanha = await _campanhaService.GetCampanhaByIdAsync(id);
            if (campanha == null)
                return NotFound();
            return Ok(campanha);
        }

        [HttpPost]
        public async Task<ActionResult<Campanha>> CreateCampanha(Campanha campanha)
        {
            var newCampanha = await _campanhaService.CreateCampanhaAsync(campanha);
            return CreatedAtAction(nameof(GetCampanhaById), new { id = newCampanha.Id }, newCampanha);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampanha(int id, Campanha campanha)
        {
            if (id != campanha.Id)
                return BadRequest();

            var updatedCampanha = await _campanhaService.UpdateCampanhaAsync(campanha);
            if (updatedCampanha == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampanha(int id)
        {
            var deleted = await _campanhaService.DeleteCampanhaAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
