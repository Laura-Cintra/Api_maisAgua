using maisAgua.Application.Repository;
using maisAgua.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using maisAgua.Application.Service;
using maisAgua.Application.Validators;
using maisAgua.Presentation.Filters;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<DomainExceptionFilter>();
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API +Água",
        Version = "V1",
        Description = "API para registro/consulta de dispositivos instalados da +Água e suas leituras",
        Contact = new OpenApiContact
        {
            Name = "Prisma.Code",
            Email = "prismacode3@gmail.com"
        },
    });
    // NÃO ESQUECER DE ADICIONAR PROPRIEDADE DE LEITURA DO ARQUIVO NO .csproj
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Cria um arquivo xml com o nome a partir do Assembly, neste caso, pega o nome 'maisAgua', ficando maisAgua.xml
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Pega o caminho do projeto atual para criar o caminho correto do arquivo. Ex: "C:\Saes\bin\Debug\net8.0\maisAgua.xml" 
    swagger.IncludeXmlComments(xmlPath); // Isso indica para o Swagger a ler o arquivo xml criado.
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"))
);

// Repositorios
builder.Services.AddScoped<DeviceRepository>();

// Validators
builder.Services.AddScoped<DeviceCreateValidator>();
builder.Services.AddScoped<DeviceUpdateValidator>();


// Services
builder.Services.AddScoped<DeviceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
