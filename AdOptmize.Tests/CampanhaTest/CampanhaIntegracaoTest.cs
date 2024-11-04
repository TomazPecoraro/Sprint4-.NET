using Xunit;
using Microsoft.EntityFrameworkCore;
using AdOptimize.Services;
using AdOptimize.Models.Models;
using AdOptimize.Models.DTOs;
using AdOptimize.Repository;
using System.Threading.Tasks;
using AutoMapper;
using System;
using AdOptimize.DataBase;

namespace AdOptimize.Tests.Integration
{
    public class CampanhaServiceIntegrationTests
    {
        private readonly IMapper _mapper;

        public CampanhaServiceIntegrationTests()
        {
            // Configuração do AutoMapper (simplificado para testes)
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Campanha, CampanhaDTO>();
                cfg.CreateMap<CampanhaDTO, Campanha>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        private DbContextOptions<OracleDbContext> CreateNewContextOptions()
        {
            // Gera um nome de banco de dados único para cada teste
            var dbName = Guid.NewGuid().ToString();
            return new DbContextOptionsBuilder<OracleDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .EnableSensitiveDataLogging() // Opcional: para logs detalhados
                .Options;
        }

        [Fact]
        public async Task GetCampanhaById_ReturnsCorrectData_WhenCampanhaExists()
        {
            // Arrange
            var options = CreateNewContextOptions();

            // Usando um contexto separado para inserir dados
            using (var context = new OracleDbContext(options))
            {
                var repository = new CampanhaRepository(context);
                var service = new CampanhaService(repository, _mapper);

                var campanha = new Campanha { Id = 1, Nome = "Test Campanha", Descricao = "Descrição da Campanha" };
                await context.Campanhas.AddAsync(campanha);
                await context.SaveChangesAsync();
            }

            // Usando um novo contexto para o teste real
            using (var context = new OracleDbContext(options))
            {
                var repository = new CampanhaRepository(context);
                var service = new CampanhaService(repository, _mapper);

                // Act
                var result = await service.GetCampanhaByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Test Campanha", result.Nome);
                Assert.Equal("Descrição da Campanha", result.Descricao);
            }
        }

        [Fact]
        public async Task AddCampanha_CreatesNewRecordInDatabase()
        {
            // Arrange
            var options = CreateNewContextOptions();

            using (var context = new OracleDbContext(options))
            {
                var repository = new CampanhaRepository(context);
                var service = new CampanhaService(repository, _mapper);

                var newCampanhaDTO = new CampanhaDTO { Nome = "Nova Campanha", Descricao = "Descrição da Nova Campanha" };

                // Act
                await service.CreateCampanhaAsync(newCampanhaDTO);
            }

            // Usando um novo contexto para verificar a inserção
            using (var context = new OracleDbContext(options))
            {
                var repository = new CampanhaRepository(context);
                var service = new CampanhaService(repository, _mapper);

                var campanhaInDb = await context.Campanhas.FirstOrDefaultAsync(c => c.Nome == "Nova Campanha");

                // Assert
                Assert.NotNull(campanhaInDb);
                Assert.Equal("Descrição da Nova Campanha", campanhaInDb.Descricao);
            }
        }
    }
}
