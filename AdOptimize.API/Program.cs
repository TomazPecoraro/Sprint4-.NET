using AdOptimize.API.Profiles;
using AdOptimize.DataBase;
using AdOptimize.Repository;
using AdOptimize.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.ML;
using System.IO;
using AdOptimize_API;

var builder = WebApplication.CreateBuilder(args);

// Inicializa Firebase
FireInitializer.Initialize();

// Configuração de dependências e serviços
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFireService, FireService>();

// Configuração do DbContext com a string de conexão
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"),
        b => b.MigrationsAssembly("AdOptimize.API")));

// Configuração de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Adiciona serviços de controladores ao contêiner
builder.Services.AddControllers();

// Configuração do Swagger com autenticação JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AdOptimize API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no campo",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
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

// Configuração de logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Para exibir exceções detalhadas em desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdOptimize API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Manipulador de erros para produção
    app.UseHsts(); // Segurança para HTTP estrito
}

// Definindo o caminho do modelo
string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "MLModel1.mlnet");
var mlContext = new MLContext();
ITransformer mlModel;

// Carregando o modelo
try
{
    mlModel = mlContext.Model.Load(modelPath, out var modelInputSchema);
}
catch (Exception ex)
{
    // Tratar exceção (por exemplo, logar erro)
    Console.WriteLine($"Erro ao carregar o modelo: {ex.Message}");
    return;
}

// Definindo a rota de predição
app.MapPost("/predict", (MLModel1.ModelInput input) =>
{
    try
    {
        // Criação da entrada de dados para o modelo
        var inputData = mlContext.Data.LoadFromEnumerable(new[] { input });

        // Fazendo a predição
        var predictions = mlModel.Transform(inputData);
        var predictedResult = mlContext.Data.CreateEnumerable<MLModel1.ModelOutput>(predictions, reuseRowObject: false).FirstOrDefault();

        return Results.Ok(predictedResult);
    }
    catch (Exception ex)
    {
        // Tratar exceção (por exemplo, retornar um erro para o cliente)
        return Results.BadRequest($"Erro na predição: {ex.Message}");
    }
});

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

app.UseAuthentication(); // Adiciona o middleware de autenticação
app.UseAuthorization();

app.MapControllers();

app.Run();
