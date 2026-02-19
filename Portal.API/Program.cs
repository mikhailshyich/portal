using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portal.Application.Services;
using Portal.Domain.Interfaces;
using Portal.Infrastructure.Data;
using Portal.Infrastructure.Repositories;
using Scalar.AspNetCore;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PortalDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Строка подключения к БД не найдена"));
    //var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    //options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString) ??
    //    throw new InvalidOperationException("Строка подключения к БД не найдена"));
});
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserDomain, UserRepository>();

builder.Services.AddScoped<IMainWarehouse, MainWarehouseService>();
builder.Services.AddScoped<IMainWarehouseDomain, MainWarehouseRepository>();

builder.Services.AddScoped<IUserDepartment, UserDepartmentService>();
builder.Services.AddScoped<IUserDepartmentDomain, UserDepartmentRepository>();

builder.Services.AddScoped<IHardware, HardwareService>();
builder.Services.AddScoped<IHardwareDomain, HardwareRepository>();

builder.Services.AddScoped<IDocumentExternalSystem, DocumentExternalSystemService>();
builder.Services.AddScoped<IDocumentExternalSystemDomain, DocumentExternalSystemRepository>();

builder.Services.AddScoped<ICategoryHardware, CategoryHardwareService>();
builder.Services.AddScoped<ICategoryHardwareDomain, CategoryHardwareRepository>();

builder.Services.AddScoped<IUserWarehouse, UserWarehouseService>();
builder.Services.AddScoped<IUserWarehouseDomain, UserWarehouseRepositiry>();

builder.Services.AddScoped<IMarkCode, MarkCodeService>();
builder.Services.AddScoped<IMarkCodeDomain, MarkCodeRepository>();

builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddScoped<IHistoryDomain, HistoryRepository>();

builder.Services.AddScoped<IKnowledgeTestService, KnowledgeTestService>();
builder.Services.AddScoped<IKnowledgeTestDomain, KnowledgeTestRepository>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        //ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
