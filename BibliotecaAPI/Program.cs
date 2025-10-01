using BibliotecaAPI;
using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Area de servicios

builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();


//builder.Services.AddTransient<IRepositorioValores, RepositorioValores>();
//builder.Services.AddTransient<IRepositorioValores, RepositorioValoresOracle>();
builder.Services.AddSingleton<IRepositorioValores, RepositorioValoresOracle>();

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<ApplicationDbContext>( options => 
    options.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

//Area de middlewares

app.UseLogueaPeticion();

app.UseBloqueaPeticion();

app.MapControllers();

app.Run();
