using AdOptimize.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
        Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
        Task<UsuarioDTO> CreateUsuarioAsync(UsuarioDTO usuarioDto);
        Task<UsuarioDTO> UpdateUsuarioAsync(UsuarioDTO usuarioDto);
        Task<bool> DeleteUsuarioAsync(int id);
    }
}
