using AdOptimize.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public interface IAnuncioService
    {
        Task<IEnumerable<Anuncio>> GetAllAnunciosAsync();
        Task<Anuncio> GetAnuncioByIdAsync(int id);
        Task<Anuncio> CreateAnuncioAsync(Anuncio anuncio);
        Task<Anuncio> UpdateAnuncioAsync(Anuncio anuncio);
        Task<bool> DeleteAnuncioAsync(int id);
    }
}
