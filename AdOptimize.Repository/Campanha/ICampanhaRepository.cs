using AdOptimize.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Repository
{
    public interface ICampanhaRepository
    {
        Task<IEnumerable<Campanha>> GetAllAsync();
        Task<Campanha> GetByIdAsync(int id);
        Task<Campanha> AddAsync(Campanha campanha);
        Task<Campanha> UpdateAsync(Campanha campanha);
        Task DeleteAsync(int id);
    }
}
