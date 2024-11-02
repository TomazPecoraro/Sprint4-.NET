using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using AdOptimize.Models.DTOs;
using AdOptimize.Services;

namespace AdOptimize.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Pega todos os usuários", Description = "Use este endpoint para puxar os valores de todos os usuários do sistema.")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Pega o valor de um usuário existente", Description = "Use este endpoint para puxar os dados de um usuário do sistema.")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound($"Usuário com ID '{id}' não encontrado.");
            }
            return Ok(usuario);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria um novo usuário", Description = "Use este endpoint para adicionar um novo usuário ao sistema.")]
        public async Task<IActionResult> Create([FromBody] UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUsuario = await _usuarioService.CreateUsuarioAsync(usuarioDTO);

                if (createdUsuario == null)
                {
                    return BadRequest("Erro ao criar o usuário.");
                }

                return CreatedAtAction(nameof(GetById), new { id = createdUsuario.Id }, createdUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Captura exceções e retorna uma mensagem de erro
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Altera um usuário existente", Description = "Use este endpoint para alterar um usuário do sistema.")]
        public async Task<IActionResult> Update(int id, [FromBody] UsuarioDTO usuarioDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usuarioService.UpdateUsuarioAsync(id, usuarioDTO);
            if (result == null)
            {
                return NotFound($"Usuário com ID '{id}' não encontrado.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deleta um usuário existente", Description = "Use este endpoint para deletar um usuário do sistema.")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usuarioService.DeleteUsuarioAsync(id);
            if (!result)
            {
                return NotFound($"Usuário com ID '{id}' não encontrado.");
            }
            return NoContent();
        }
    }
}
