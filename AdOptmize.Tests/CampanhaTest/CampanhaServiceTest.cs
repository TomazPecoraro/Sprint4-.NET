using Xunit;
using Moq;
using AdOptimize.Services;
using AdOptimize.Models.Models;
using AdOptimize.Repository;
using System.Threading.Tasks;

namespace AdOptimize.Tests
{
    public class CampanhaServiceTests
    {
        private readonly Mock<ICampanhaRepository> _mockRepository;
        private readonly CampanhaService _service;

        public CampanhaServiceTests()
        {
            _mockRepository = new Mock<ICampanhaRepository>();
            _service = new CampanhaService(_mockRepository.Object, null);
        }

        [Fact]
        public async Task GetCampanhaById_ReturnsCampanha_WhenCampanhaExists()
        {
            // Arrange
            var campanhaId = 1;
            var expectedCampanha = new Campanha { Id = campanhaId, Nome = "Test Campanha" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(campanhaId)).ReturnsAsync(expectedCampanha);

            // Act
            var result = await _service.GetCampanhaByIdAsync(campanhaId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCampanha.Nome, result.Nome);
        }
    }
}
