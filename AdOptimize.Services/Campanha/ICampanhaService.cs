using AdOptimize.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public interface ICampanhaService
    {
        Task<IEnumerable<CampanhaDTO>> GetAllCampanhasAsync();
        Task<CampanhaDTO> GetCampanhaByIdAsync(int id);
        Task<CampanhaDTO> CreateCampanhaAsync(CampanhaDTO campanhaDto);
        Task<CampanhaDTO> UpdateCampanhaAsync(CampanhaDTO campanhaDto);
        Task<bool> DeleteCampanhaAsync(int id);
    }
}


