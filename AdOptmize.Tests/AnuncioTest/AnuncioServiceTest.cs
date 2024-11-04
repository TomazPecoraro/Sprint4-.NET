using Xunit;
using Moq;
using AdOptimize.Services;
using AdOptimize.Models.Models;
using AdOptimize.Models.DTOs;
using AdOptimize.Repository;
using System.Threading.Tasks;
using AutoMapper;

namespace AdOptimize.Tests
{
    public class AnuncioServiceTests
    {
        private readonly Mock<IAnuncioRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AnuncioService _service;

        public AnuncioServiceTests()
        {
            _mockRepository = new Mock<IAnuncioRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new AnuncioService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAnuncioById_ReturnsAnuncioDTO_WhenAnuncioExists()
        {
            // Arrange
            var anuncioId = 1;
            var expectedAnuncio = new Anuncio { Id = anuncioId, Titulo = "Test Anuncio" };
            var expectedAnuncioDTO = new AnuncioDTO { Id = anuncioId, Titulo = "Test Anuncio" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(anuncioId)).ReturnsAsync(expectedAnuncio);

            _mockMapper.Setup(m => m.Map<AnuncioDTO>(expectedAnuncio)).Returns(expectedAnuncioDTO);

            // Act
            var result = await _service.GetAnuncioByIdAsync(anuncioId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AnuncioDTO>(result);
            Assert.Equal(expectedAnuncioDTO.Titulo, result.Titulo);
        }
    }
}
