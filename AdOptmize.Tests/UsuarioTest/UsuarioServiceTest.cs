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
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UsuarioService _service;

        public UsuarioServiceTests()
        {
            _mockRepository = new Mock<IUsuarioRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new UsuarioService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetUsuarioById_ReturnsUsuarioDTO_WhenUsuarioExists()
        {
            // Arrange
            var usuarioId = 1;
            var expectedUsuario = new Usuario { Id = usuarioId, Nome = "Test Usuario" };
            var expectedUsuarioDTO = new UsuarioDTO { Id = usuarioId, Nome = "Test Usuario" };

            _mockRepository.Setup(repo => repo.GetByIdAsync(usuarioId)).ReturnsAsync(expectedUsuario);

            _mockMapper.Setup(m => m.Map<UsuarioDTO>(expectedUsuario)).Returns(expectedUsuarioDTO);

            // Act
            var result = await _service.GetUsuarioByIdAsync(usuarioId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UsuarioDTO>(result);
            Assert.Equal(expectedUsuarioDTO.Nome, result.Nome);
        }
    }
}
