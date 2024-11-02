using AdOptimize.Models.Models;
using AdOptimize.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IAnuncioRepository _anuncioRepository;

        public AnuncioService(IAnuncioRepository anuncioRepository)
        {
            _anuncioRepository = anuncioRepository;
        }

        public async Task<IEnumerable<Anuncio>> GetAllAnunciosAsync()
        {
            return await _anuncioRepository.GetAllAsync();
        }

        public async Task<Anuncio> GetAnuncioByIdAsync(int id)
        {
            return await _anuncioRepository.GetByIdAsync(id);
        }

        public async Task<Anuncio> CreateAnuncioAsync(Anuncio anuncio)
        {
            return await _anuncioRepository.AddAsync(anuncio);
        }

        public async Task<Anuncio> UpdateAnuncioAsync(Anuncio anuncio)
        {
            var existingAnuncio = await _anuncioRepository.GetByIdAsync(anuncio.Id);
            if (existingAnuncio == null)
            {
                return null;
            }

            return await _anuncioRepository.UpdateAsync(anuncio);
        }

        public async Task<bool> DeleteAnuncioAsync(int id)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(id);
            if (anuncio == null)
            {
                return false;
            }

            await _anuncioRepository.DeleteAsync(id);
            return true;
        }
    }
}
