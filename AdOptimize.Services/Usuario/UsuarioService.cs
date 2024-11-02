using AdOptimize.Models.Models;
using AdOptimize.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            return await _usuarioRepository.GetByIdAsync(id);
        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            return await _usuarioRepository.AddAsync(usuario);
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            var existingUsuario = await _usuarioRepository.GetByIdAsync(usuario.Id);
            if (existingUsuario == null)
                return null;

            return await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            if (usuario == null)
                return false;

            await _usuarioRepository.DeleteAsync(id);
            return true;
        }
    }
}
