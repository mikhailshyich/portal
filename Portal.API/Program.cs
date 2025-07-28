using Microsoft.EntityFrameworkCore;
using Portal.Application.Services;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Data;
using Portal.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PortalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Строка подключения к БД не найдена"));
});

builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IGame, GameRepository>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
