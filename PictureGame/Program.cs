using Microsoft.EntityFrameworkCore;
using PictureGame.Infrastructure.Data;
using PictureGame;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add the DbContext to the container
builder.Services.AddDbContext<GameContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionString"))
	);

// Add the IUserProvider interface and the UserProvider class as a service
builder.Services.AddScoped<IUserProvider, UserProvider>(); 
builder.Services.AddScoped<IUserValidator, UserValidator>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
