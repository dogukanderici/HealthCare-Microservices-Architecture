using Core.AdminUI.Middlewares.BearerMiddleware;
using Core.WebUI.Application.Commons.Parameters;
using Core.WebUI.Configuration.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

// Cookie Authentication Configurations
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
        options.Cookie.Name = "CoreAdminUICookie";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

// DelegatingHandler Configuration
builder.Services.AddTransient<AccessTokenDelegationHandler>();

// HttpClient Service Configurations
builder.Services.AddHttpClient("IdentityServerClient", cfg =>
{
    cfg.BaseAddress = new Uri(builder.Configuration["ApiSetting:IdentityServerBaseUrl"]);
});

//builder.Services.AddHttpClient("IdentityServerClient", cfg =>
//{
//    cfg.BaseAddress = new Uri(builder.Configuration["ApiSetting:IdentityServerBaseUrl"]);
//}).AddHttpMessageHandler<AccessTokenDelegationHandler>();

// Services Configuration
builder.Services.AddHttpClientServices();

builder.Services.Configure<ApiSetting>(builder.Configuration.GetSection("ApiSetting"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
