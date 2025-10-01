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

app.Use(async (contexto, next) =>
{
    //Cuando viene la peticion
    var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation($"Peticion: {contexto.Request.Method} {contexto.Request.Path}");

    await next.Invoke();

    logger.LogInformation($"Respuesta: {contexto.Response.StatusCode}");
});

app.Use(async (contexto, next) =>
{
    if (contexto.Request.Path == "/bloqueado")
    {
        contexto.Response.StatusCode = 403;
        await contexto.Response.WriteAsync("Acceso denegado");
    }
    else
    {
        await next.Invoke();
    }
});

app.MapControllers();

app.Run();
