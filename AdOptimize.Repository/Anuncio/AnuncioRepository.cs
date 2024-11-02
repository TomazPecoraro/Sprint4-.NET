using AdOptimize.DataBase;
using AdOptimize.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdOptimize.Repository
{
    public class AnuncioRepository : IAnuncioRepository
    {
        private readonly OracleDbContext _context;

        public AnuncioRepository(OracleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Anuncio>> GetAllAsync()
        {
            return await _context.Anuncios.ToListAsync();
        }

        public async Task<Anuncio> GetByIdAsync(int id)
        {
            return await _context.Anuncios.FindAsync(id);
        }

        public async Task<Anuncio> AddAsync(Anuncio anuncio)
        {
            await _context.Anuncios.AddAsync(anuncio);
            await _context.SaveChangesAsync();
            return anuncio;
        }

        public async Task<Anuncio> UpdateAsync(Anuncio anuncio)
        {
            _context.Anuncios.Update(anuncio);
            await _context.SaveChangesAsync();
            return anuncio;
        }

        public async Task DeleteAsync(int id)
        {
            var anuncio = await GetByIdAsync(id);
            if (anuncio != null)
            {
                _context.Anuncios.Remove(anuncio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
