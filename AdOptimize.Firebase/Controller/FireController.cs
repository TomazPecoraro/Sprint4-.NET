using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class CampanhaController : ControllerBase
{
    private readonly IFirestoreService _firestoreService;

    public CampanhaController(IFirestoreService firestoreService)
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

        await _firestoreService.AddCampanha(campanha);
        return CreatedAtAction(nameof(CreateCampanha), new { id = campanha.Id }, campanha);
    }
}
