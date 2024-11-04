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

// Configura��o de depend�ncias e servi�os
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFireService, FireService>();

// Configura��o do DbContext com a string de conex�o
builder.Services.AddDbContext<OracleDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"),
        b => b.MigrationsAssembly("AdOptimize.API")));

// Configura��o de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Adiciona servi�os de controladores ao cont�iner
builder.Services.AddControllers();

// Configura��o do Swagger com autentica��o JWT
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

// Registro dos servi�os
builder.Services.AddScoped<IAnuncioService, AnuncioService>();
builder.Services.AddScoped<ICampanhaService, CampanhaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAnuncioRepository, AnuncioRepository>();
builder.Services.AddScoped<ICampanhaRepository, CampanhaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

// Adiciona servi�os de autoriza��o
builder.Services.AddAuthorization();

// Configura��o de logs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Para exibir exce��es detalhadas em desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AdOptimize API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Manipulador de erros para produ��o
    app.UseHsts(); // Seguran�a para HTTP estrito
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
    // Tratar exce��o (por exemplo, logar erro)
    Console.WriteLine($"Erro ao carregar o modelo: {ex.Message}");
    return;
}

// Definindo a rota de predi��o
app.MapPost("/predict", (MLModel1.ModelInput input) =>
{
    try
    {
        // Cria��o da entrada de dados para o modelo
        var inputData = mlContext.Data.LoadFromEnumerable(new[] { input });

        // Fazendo a predi��o
        var predictions = mlModel.Transform(inputData);
        var predictedResult = mlContext.Data.CreateEnumerable<MLModel1.ModelOutput>(predictions, reuseRowObject: false).FirstOrDefault();

        return Results.Ok(predictedResult);
    }
    catch (Exception ex)
    {
        // Tratar exce��o (por exemplo, retornar um erro para o cliente)
        return Results.BadRequest($"Erro na predi��o: {ex.Message}");
    }
});

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");

app.UseAuthentication(); // Adiciona o middleware de autentica��o
app.UseAuthorization();

app.MapControllers();

app.Run();
