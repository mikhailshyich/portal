using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using Portal.WEB.Authentication;
using Portal.WEB.Components;
using Portal.WEB.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddScoped(sp =>
{
    //return new HttpClient { BaseAddress = new Uri("http://192.168.1.47:81") };
    //return new HttpClient { BaseAddress = new Uri("http://apitmk.site") };
    return new HttpClient { BaseAddress = new Uri("https://localhost:7266") };
});

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserServiceWEB>();
builder.Services.AddScoped<UserDepartmentServiceWEB>();
builder.Services.AddScoped<CategoryHardwareServiceWEB>();
builder.Services.AddScoped<DocumentExternalSystemServiceWEB>();
builder.Services.AddScoped<MainWarehouseServiceWEB>();
builder.Services.AddScoped<HardwareServiceWEB>();
builder.Services.AddScoped<UserWarehouseServiceWEB>();
builder.Services.AddScoped<HistoryServiceWEB>();



builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/401");

app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
