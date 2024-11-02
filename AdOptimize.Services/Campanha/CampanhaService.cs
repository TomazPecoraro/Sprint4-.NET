using AdOptimize.Models.DTOs;
using AdOptimize.Models.Models;
using AdOptimize.Repository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public class CampanhaService : ICampanhaService
    {
        private readonly ICampanhaRepository _campanhaRepository;
        private readonly IMapper _mapper;

        public CampanhaService(ICampanhaRepository campanhaRepository, IMapper mapper)
        {
            _campanhaRepository = campanhaRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CampanhaDTO>> GetAllCampanhasAsync()
        {
            var campanhas = await _campanhaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CampanhaDTO>>(campanhas);
        }

        public async Task<CampanhaDTO> GetCampanhaByIdAsync(int id)
        {
            var campanha = await _campanhaRepository.GetByIdAsync(id);
            return _mapper.Map<CampanhaDTO>(campanha);
        }

        public async Task<CampanhaDTO> CreateCampanhaAsync(CampanhaDTO campanhaDto)
        {
            var campanha = _mapper.Map<Campanha>(campanhaDto);
            var newCampanha = await _campanhaRepository.AddAsync(campanha);
            return _mapper.Map<CampanhaDTO>(newCampanha);
        }

        public async Task<CampanhaDTO> UpdateCampanhaAsync(CampanhaDTO campanhaDto)
        {
            var campanha = _mapper.Map<Campanha>(campanhaDto);
            var updatedCampanha = await _campanhaRepository.UpdateAsync(campanha);
            return _mapper.Map<CampanhaDTO>(updatedCampanha);
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
