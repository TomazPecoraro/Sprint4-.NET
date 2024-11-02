using AdOptimize.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public interface IAnuncioService
    {
        Task<IEnumerable<AnuncioDTO>> GetAllAnunciosAsync();
        Task<AnuncioDTO> GetAnuncioByIdAsync(int id);
        Task<AnuncioDTO> CreateAnuncioAsync(AnuncioDTO anuncioDto);
        Task<AnuncioDTO> UpdateAnuncioAsync(int id, AnuncioDTO anuncioDto); 
        Task<bool> DeleteAnuncioAsync(int id);
    }
}
