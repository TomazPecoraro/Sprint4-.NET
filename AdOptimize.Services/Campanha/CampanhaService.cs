using AdOptimize.Models.Models;
using AdOptimize.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public class CampanhaService : ICampanhaService
    {
        private readonly ICampanhaRepository _campanhaRepository;

        public CampanhaService(ICampanhaRepository campanhaRepository)
        {
            _campanhaRepository = campanhaRepository;
        }

        public async Task<IEnumerable<Campanha>> GetAllCampanhasAsync()
        {
            return await _campanhaRepository.GetAllAsync();
        }

        public async Task<Campanha> GetCampanhaByIdAsync(int id)
        {
            return await _campanhaRepository.GetByIdAsync(id);
        }

        public async Task<Campanha> CreateCampanhaAsync(Campanha campanha)
        {
            return await _campanhaRepository.AddAsync(campanha);
        }

        public async Task<Campanha> UpdateCampanhaAsync(Campanha campanha)
        {
            var existingCampanha = await _campanhaRepository.GetByIdAsync(campanha.Id);
            if (existingCampanha == null)
                return null;

            return await _campanhaRepository.UpdateAsync(campanha);
        }

        public async Task<bool> DeleteCampanhaAsync(int id)
        {
            var campanha = await _campanhaRepository.GetByIdAsync(id);
            if (campanha == null)
                return false;

            await _campanhaRepository.DeleteAsync(id);
            return true;
        }
    }
}
