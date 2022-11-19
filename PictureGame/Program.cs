using System;
using System.Globalization;
using System.IO;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PictureGame;
using PictureGame.SharedKernel;
using PictureGame.Infrastructure.Data;
using PictureGame.Core.Domain.Game;

var builder = WebApplication.CreateBuilder();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromSeconds(60); // We're keeping this low to facilitate testing. Would normally be higher. Default is 20 minutes
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.IsEssential = true;              // Otherwise we need cookie approval
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<GameContext>(options =>
{
    options.UseSqlite($"Data Source={Path.Combine("Infrastructure", "Data", "Game.db")}");
});

builder.Services.AddMediatR(typeof(Program));

builder.Services.Scan(scan => scan
    .FromCallingAssembly()
        .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
        .AsImplementedInterfaces());

builder.Services.AddScoped<IGetRandomImageService, GetRandomImageService>();

builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    if (app.Environment.IsDevelopment())
    {
        var db = scope.ServiceProvider.GetRequiredService<GameContext>();
    }
}

app.UseHttpsRedirection();

var supportedCultures = new[]
{
            new CultureInfo("en-GB"),
        };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-GB"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();


app.Run();

public partial class Program { }