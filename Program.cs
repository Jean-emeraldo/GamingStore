using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GamingStore.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services MVC
builder.Services.AddControllersWithViews();

// Connexion à la base de données MySQL
var connectionString = "Server=localhost;Port=3306;Database=AppWebDB;User=root;Password=;";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Initialisation de la base de données au démarrage
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate(); // Mise à jour de la base
    DbInitializer.Initialize(context); // Ajout de données de test, si besoin
}

// Configuration des routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=NewPage}/{id?}");

app.Run();
