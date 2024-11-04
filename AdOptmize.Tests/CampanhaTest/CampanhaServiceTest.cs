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
    public class CampanhaServiceTests
    {
        private readonly Mock<ICampanhaRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CampanhaService _service;

        public CampanhaServiceTests()
        {
            _mockRepository = new Mock<ICampanhaRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CampanhaService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetCampanhaById_ReturnsCampanhaDTO_WhenCampanhaExists()
        {
            // Arrange
            var campanhaId = 1;
            var expectedCampanha = new Campanha { Id = campanhaId, Nome = "Test Campanha" };
            var expectedCampanhaDTO = new CampanhaDTO { Id = campanhaId, Nome = "Test Campanha" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(campanhaId)).ReturnsAsync(expectedCampanha);

            // Configuração do mock do mapeamento para retornar o DTO esperado
            _mockMapper.Setup(m => m.Map<CampanhaDTO>(expectedCampanha)).Returns(expectedCampanhaDTO);

            // Act
            var result = await _service.GetCampanhaByIdAsync(campanhaId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CampanhaDTO>(result);  // Verifica se o retorno é CampanhaDTO
            Assert.Equal(expectedCampanhaDTO.Nome, result.Nome);
        }
    }
}
