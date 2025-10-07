using BibliotecaAPI;
using BibliotecaAPI.Datos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var diccionarioConfiguraciones = new Dictionary<string, string>
{
    {"ambiente", "Un diccionario en memoria" }
};

//Area de servicios

builder.Services.AddOptions<PersonaOpciones>()
    .Bind(builder.Configuration.GetSection(PersonaOpciones.Seccion))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<TarifaOpciones>()
    .Bind(builder.Configuration.GetSection(TarifaOpciones.Seccion))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<PagosProcesamiento>();

builder.Services.AddAutoMapper(typeof(Program));

//builder.Services.AddControllers().AddJsonOptions(options =>
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddDbContext<ApplicationDbContext>( options => 
    options.UseSqlServer("name=DefaultConnection"));

var app = builder.Build();

//Area de middlewares

app.MapControllers();

app.Run();
