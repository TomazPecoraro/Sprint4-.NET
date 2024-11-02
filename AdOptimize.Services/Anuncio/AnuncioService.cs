using AdOptimize.Models.DTOs;
using AdOptimize.Models.Models;
using AdOptimize.Repository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public class AnuncioService : IAnuncioService
    {
        private readonly IAnuncioRepository _anuncioRepository;
        private readonly IMapper _mapper;

        public AnuncioService(IAnuncioRepository anuncioRepository, IMapper mapper)
        {
            _anuncioRepository = anuncioRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnuncioDTO>> GetAllAnunciosAsync()
        {
            var anuncios = await _anuncioRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AnuncioDTO>>(anuncios);
        }

        public async Task<AnuncioDTO> GetAnuncioByIdAsync(int id)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(id);
            return _mapper.Map<AnuncioDTO>(anuncio);
        }

        public async Task<AnuncioDTO> CreateAnuncioAsync(AnuncioDTO anuncioDto)
        {
            var anuncio = _mapper.Map<Anuncio>(anuncioDto);
            var newAnuncio = await _anuncioRepository.AddAsync(anuncio);
            return _mapper.Map<AnuncioDTO>(newAnuncio);
        }

        public async Task<AnuncioDTO> UpdateAnuncioAsync(int id, AnuncioDTO anuncioDto)
        {
            var anuncio = _mapper.Map<Anuncio>(anuncioDto);
            var updatedAnuncio = await _anuncioRepository.UpdateAsync(anuncio);
            return _mapper.Map<AnuncioDTO>(updatedAnuncio);
        }

        public async Task<bool> DeleteAnuncioAsync(int id)
        {
            var anuncio = await _anuncioRepository.GetByIdAsync(id);
            if (anuncio == null)
                return false;

            await _anuncioRepository.DeleteAsync(id);
            return true;
        }
    }
}
