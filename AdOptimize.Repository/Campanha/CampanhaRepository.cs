using AdOptimize.DataBase;
using AdOptimize.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Repository
{
    public class CampanhaRepository : ICampanhaRepository
    {
        private readonly OracleDbContext _context;

        public CampanhaRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Campanha>> GetAllAsync()
        {
            return await _context.Campanhas.Include(c => c.Anuncios).ToListAsync();
        }

        public async Task<Campanha> GetByIdAsync(int id)
        {
            return await _context.Campanhas
                .Include(c => c.Anuncios)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Campanha> AddAsync(Campanha campanha)
        {
            await _context.Campanhas.AddAsync(campanha);
            await _context.SaveChangesAsync();
            return campanha;
        }

        public async Task<Campanha> UpdateAsync(Campanha campanha)
        {
            _context.Campanhas.Update(campanha);
            await _context.SaveChangesAsync();
            return campanha;
        }

        public async Task DeleteAsync(int id)
        {
            var campanha = await GetByIdAsync(id);
            if (campanha != null)
            {
                _context.Campanhas.Remove(campanha);
                await _context.SaveChangesAsync();
            }
        }
    }
}
