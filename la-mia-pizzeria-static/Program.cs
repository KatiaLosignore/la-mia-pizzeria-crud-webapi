using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("PizzaContextConnection") ?? throw new InvalidOperationException("Connection string 'PizzaContextConnection' not found.");


builder.Services.AddDbContext<PizzaContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PizzaContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Codice di cofigurazione per il serializzatore JSON, in modo che ignori completamente le dipendenze cicliche di
// eventuali relazione N:N o 1:N presenti nel JSON risultante.
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Righe scritte nel live coding (Argomento Dependency Injection)
builder.Services.AddScoped<ICustomLogger, CustomFileLogger>();
builder.Services.AddScoped<PizzaContext, PizzaContext>();
builder.Services.AddScoped<IRepositoryPizzas, RepositoryPizzas>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pizza}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
