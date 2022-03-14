using Dopomoga.Data;
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

builder.Services.Configure<RequestLocalizationOptions>(opt =>
{
    var supportedCultures = new List<CultureInfo>()
    {
        new CultureInfo("en"),
        new CultureInfo("uk"),
    };

    opt.DefaultRequestCulture = new RequestCulture("en");
    opt.SupportedCultures = supportedCultures;
    opt.SupportedUICultures = supportedCultures;
});

builder.Services.AddScoped<RequestLocalizationCookiesMiddleware>();


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<DopomogaDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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

//var supportedLocalizations = new[] { "ge", "uk" };
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedLocalizations[0])
//    .AddSupportedCultures(supportedLocalizations)
//    .AddSupportedUICultures(supportedLocalizations);

//app.UseRequestLocalization(localizationOptions);
//app.UseRequestLocalizationCookies();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
