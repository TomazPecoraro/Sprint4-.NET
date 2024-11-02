using AdOptimize.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Repository
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> AddAsync(Usuario usuario);
        Task<Usuario> UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
    }
}
