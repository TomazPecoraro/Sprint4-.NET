using Xunit;
using Microsoft.EntityFrameworkCore;
using AdOptimize.Repository;
using AdOptimize.Models.Models;
using System.Threading.Tasks;
using AdOptimize.DataBase;

namespace AdOptimize.Tests
{
    public class UsuarioServiceIntegrationTests
    {
        private readonly OracleDbContext _context;
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioServiceIntegrationTests()
        {
            var options = new DbContextOptionsBuilder<OracleDbContext>()
                .UseInMemoryDatabase("TestDatabase_Usuario")
                .Options;

            _context = new OracleDbContext(options);
            _usuarioRepository = new UsuarioRepository(_context);
        }

        [Fact]
        public async Task AddUsuario_CreatesNewRecordInDatabase()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Usuario Teste", Email = "usuario@teste.com" };

            // Act
            await _usuarioRepository.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Assert
            var usuarioFromDb = await _usuarioRepository.GetByIdAsync(usuario.Id);
            Assert.NotNull(usuarioFromDb);
            Assert.Equal("Usuario Teste", usuarioFromDb.Nome);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsCorrectData_WhenUsuarioExists()
        {
            // Arrange
            var usuario = new Usuario { Nome = "Usuario Teste", Email = "usuario@teste.com" };
            await _usuarioRepository.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _usuarioRepository.GetByIdAsync(usuario.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Usuario Teste", result.Nome);
        }
    }
}
