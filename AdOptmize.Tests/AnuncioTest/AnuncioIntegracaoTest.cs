using Xunit;
using Microsoft.EntityFrameworkCore;
using AdOptimize.Repository;
using AdOptimize.Models.Models;
using System.Threading.Tasks;
using AdOptimize.DataBase;

namespace AdOptimize.Tests
{
    public class AnuncioServiceIntegrationTests
    {
        private readonly OracleDbContext _context;
        private readonly AnuncioRepository _anuncioRepository;

        public AnuncioServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<OracleDbContext>()
                .UseInMemoryDatabase("TestDatabase_Anuncio")
                .Options;

            _context = new OracleDbContext(options);
            _anuncioRepository = new AnuncioRepository(_context);
        }

        [Fact]
        public async Task AddAnuncio_CreatesNewRecordInDatabase()
        {
            // Arrange
            var anuncio = new Anuncio { Titulo = "Anuncio Teste", Descricao = "Descricao Teste" };

            // Act
            await _anuncioRepository.AddAsync(anuncio);
            await _context.SaveChangesAsync();

            // Assert
            var anuncioFromDb = await _anuncioRepository.GetByIdAsync(anuncio.Id);
            Assert.NotNull(anuncioFromDb);
            Assert.Equal("Anuncio Teste", anuncioFromDb.Titulo);
        }

        [Fact]
        public async Task GetAnuncioById_ReturnsCorrectData_WhenAnuncioExists()
        {
            // Arrange
            var anuncio = new Anuncio { Titulo = "Anuncio Teste", Descricao = "Descricao Teste" };
            await _anuncioRepository.AddAsync(anuncio);
            await _context.SaveChangesAsync();

            // Act
            var result = await _anuncioRepository.GetByIdAsync(anuncio.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Anuncio Teste", result.Titulo);
        }
    }
}
