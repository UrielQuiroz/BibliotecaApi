using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/configuraciones")]
    public class ConfiguracionesController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IConfigurationSection seccion_1;
        private readonly IConfigurationSection seccion_2;

        public ConfiguracionesController(IConfiguration configuration)
        {
            this.configuration = configuration;
            seccion_1 = configuration.GetSection("seccion_1");
            seccion_2 = configuration.GetSection("seccion_2");
        }

        [HttpGet("seccion_01")]
        public ActionResult GetSeccion01()
        {
            var nombre = seccion_1["jugador"];
            var edad = seccion_1["edad"];

            return Ok( new { nombre, edad });
        }

        [HttpGet("seccion_02")]
        public ActionResult GetSeccion02()
        {
            var nombre = seccion_2["jugador"];
            var edad = seccion_2["edad"];

            return Ok(new { nombre, edad });
        }

        [HttpGet("obtener-todos")]
        public ActionResult GetAll()
        {
            //var hijos = configuration.GetChildren().Select(x => $"{x.Key}: {x.Value}");
            var hijos = seccion_1.GetChildren().Select(x => $"{x.Key}: {x.Value}");

            return Ok(new { hijos });
        }

        [HttpGet("proveedor")]
        public ActionResult GetProveedor()
        {
            var valor = configuration.GetValue<string>("ambiente");
            return Ok(new { valor });
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var opcion1 = configuration["MejorJugador"]!;
            var opcion2 = configuration.GetValue<string>("MejorJugador")!;
            return opcion2;
        }

        [HttpGet("secciones")] 
        public ActionResult<string> GetSeccion()
        {
            var opcion1 = configuration["ConnectionStrings:DefaultConnection"]!;
            var opcion2 = configuration.GetValue<string>("ConnectionStrings:DefaultConnection")!;

            var seccion = configuration.GetSection("ConnectionStrings");
            var opcion3 = seccion["DefaultConnection"]!;

            return opcion3;
        }
    }
}
