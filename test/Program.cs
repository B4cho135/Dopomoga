using Dopomoga.API.SDK;
using Dopomoga.Data;
using Dopomoga.Services;
using Dopomoga.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;
using test.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.AddMvc()
    .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IHttpClientSettings, HttpClientSettings>();
builder.Services.AddScoped<ServiceHttpClient>();
builder.Services.AddScoped<ApiClient>();
builder.Services.AddHttpContextAccessor();

var supportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("uk"),
        new CultureInfo("en"),
        new CultureInfo("ge"),
    };

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders = new List<IRequestCultureProvider>
        {
            new QueryStringRequestCultureProvider(),
            new CookieRequestCultureProvider()
        };
});

//builder.Services.AddScoped<RequestLocalizationCookiesMiddleware>();


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

var options = ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(options.Value);

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
