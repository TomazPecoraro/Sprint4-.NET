using AdOptimize.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Services
{
    public interface ICampanhaService
    {
        Task<IEnumerable<Campanha>> GetAllCampanhasAsync();
        Task<Campanha> GetCampanhaByIdAsync(int id);
        Task<Campanha> CreateCampanhaAsync(Campanha campanha);
        Task<Campanha> UpdateCampanhaAsync(Campanha campanha);
        Task<bool> DeleteCampanhaAsync(int id);
    }
}

