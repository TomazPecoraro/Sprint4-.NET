using AdOptimize.Models.DTOs;
using AdOptimize.Models.Models;
using AdOptimize.Repository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);
            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> CreateUsuarioAsync(UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var newUsuario = await _usuarioRepository.AddAsync(usuario);
            return _mapper.Map<UsuarioDTO>(newUsuario);
        }

        public async Task<UsuarioDTO> UpdateUsuarioAsync(int id, UsuarioDTO usuarioDto)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            var updatedUsuario = await _usuarioRepository.UpdateAsync(usuario);
            return _mapper.Map<UsuarioDTO>(updatedUsuario);
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
