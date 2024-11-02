using AdOptimize.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Repository
{
    public interface IAnuncioRepository
    {
        Task<IEnumerable<Anuncio>> GetAllAsync();
        Task<Anuncio> GetByIdAsync(int id);
        Task<Anuncio> AddAsync(Anuncio anuncio);
        Task<Anuncio> UpdateAsync(Anuncio anuncio);
        Task DeleteAsync(int id);
    }
}

