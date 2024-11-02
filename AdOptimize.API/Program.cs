using AdOptimize.API.Profiles;
using AdOptimize.Services;
using AdOptimize.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AdOptimize.DataBase;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com a string de conexão
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"),
        b => b.MigrationsAssembly("AdOptimize.API")));

// Adiciona serviços de controladores ao contêiner
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdOptimize API", Version = "v1" });
});

// Adiciona AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Registro dos serviços
builder.Services.AddScoped<IAnuncioService, AnuncioService>();
builder.Services.AddScoped<ICampanhaService, CampanhaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAnuncioRepository, AnuncioRepository>();
builder.Services.AddScoped<ICampanhaRepository, CampanhaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Adiciona serviços de autorização
builder.Services.AddAuthorization();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdOptimize API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Mapeia os controladores para o pipeline

app.Run();
