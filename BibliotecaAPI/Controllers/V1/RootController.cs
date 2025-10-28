using BibliotecaAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v1")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = "ObtenerRootV1")]
        public IEnumerable<DatosHATEOASDTO> Get()
        {
            var datosHATEOAS = new List<DatosHATEOASDTO>();

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("ObtenerRootV1", new { })!,
                Descripcion: "self",
                Metodo: "GET"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("ObtenerAutoresV1", new { })!,
                Descripcion: "autores-obtener",
                Metodo: "GET"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("CrearAutorV1", new { })!,
                Descripcion: "autor-crear",
                Metodo: "POST"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("CrearAutoresV1", new { })!,
                Descripcion: "autores-crear",
                Metodo: "POST"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("CrearLibroV1", new { })!,
                Descripcion: "libro-crear",
                Metodo: "POST"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("ObtenerUsuariosV1", new { })!,
                Descripcion: "usuarios-obtener",
                Metodo: "GET"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("RegistrarUsuarioV1", new { })!,
                Descripcion: "usuarios-registrar",
                Metodo: "POST"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("LoginUsuarioV1", new { })!,
                Descripcion: "usuario-login",
                Metodo: "POST"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("ActualizarUsuarioV1", new { })!,
                Descripcion: "usuario-actualizar",
                Metodo: "PUT"
            ));

            datosHATEOAS.Add(new DatosHATEOASDTO(
                Enlace: Url.Link("RenovarTokenV1", new { })!,
                Descripcion: "token-renovar",
                Metodo: "GET"
            ));

            return datosHATEOAS; 
        }
    }
}
