using BibliotecaAPI.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/valores")]
    public class ValoresController : ControllerBase
    {
        private readonly RepositorioValores repositorioValores;

        public ValoresController(RepositorioValores repositorioValores)
        {
            this.repositorioValores = repositorioValores;
        }

        [HttpGet]
        public IEnumerable<Valor> Get()
        {
            return repositorioValores.ObtenerValores();
        }
    }
}
