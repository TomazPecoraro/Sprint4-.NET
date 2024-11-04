using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AdOptimize.Models.Models;

[ApiController]
[Route("api/[controller]")]
public class FireController : ControllerBase
{
    private readonly IFireService _firestoreService;

    public FireController(IFireService firestoreService)
    {
        _firestoreService = firestoreService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCampanha([FromBody] Campanha campanha)
    {
        if (campanha == null)
        {
            return BadRequest("Campanha inválida.");
        }

        try
        {
            await _firestoreService.AddCampanha(campanha);

            if (campanha.Id == null)
            {
                return StatusCode(500, "Erro ao gerar o ID da campanha.");
            }

            return CreatedAtAction(nameof(CreateCampanha), new { id = campanha.Id }, campanha);
        }
        catch (Exception ex)
        {
            // Log da exceção, caso um mecanismo de logging esteja implementado
            return StatusCode(500, $"Erro ao criar campanha: {ex.Message}");
        }
    }
}
