using maisAgua.Application.Repository;
using maisAgua.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using maisAgua.Application.Service;
using maisAgua.Application.Validators;
using maisAgua.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<DomainExceptionFilter>();
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle"))
);

// Repositorio
builder.Services.AddScoped<DeviceRepository>();

// Validator
builder.Services.AddScoped<DeviceCreateValidator>();

// Fluent Validation
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
