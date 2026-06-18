using Duende.IdentityServer.Validation;
using Core.IdentityServer;
using Core.IdentityServer.Context;
using Core.IdentityServer.Models;
using Core.IdentityServer.Utilities;
using Core.IdentityServer.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

// SeriLog Configuration
var logPath = "/app/logs/identityserver";
Directory.CreateDirectory(logPath); // Log dosyalarýnýn kaydedileceði klasörü oluþturur.

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.File(
        Path.Combine(logPath, "log-.txt"), // Log dosyalarýnýn adýný ve konumunu belirtir.
        rollingInterval: RollingInterval.Day // Log dosyalarýnýn günlük olarak döndürülmesini saðlar.
    )
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.

// IdentityServer kendi API'sini bu yöntem ile kendi içinde JWT doðrulamasý yaparak koruyabilir.
builder.Services.AddLocalApiAuthentication();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// IdentityServerDBContext Tanýmý
builder.Services.AddDbContext<IdentityServerDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityServerDBSetting"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<IdentityServerDBContext>()
    .AddDefaultTokenProviders();

// Config dosyasý tanýmlarý
builder.Services.AddIdentityServer(opt =>
{
    opt.IssuerUri = "http://localhost:5001"; // Token'ýn hangi URL tarafýndan verildiðini belirtir. Prod ortamýnda gerçek domain adresi olmalý.
})
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential() // Dev için eklenir. Prod'da olmamalý.
    .AddProfileService<CustomUserProfileService>(); // Token'a kullanýcý rollerini eklemek için kullanýlýr.

builder.Services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseIdentityServer();
//app.UseAuthentication(); // Duende IdentityServer yeni versiyonu ile bu ekleme göülü olarak yapýlýyor.
app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Application starting...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An error occured while application is starting!");
}
finally
{
    // Uygulama kapanýrken veya crash olduðunda bellekteki tüm loglarý dosyaya yazýlýr.
    Log.CloseAndFlush();
}
