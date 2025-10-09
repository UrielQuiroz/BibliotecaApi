using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [ApiController]
    [Route("api/seguridad")]
    public class SeguridadController : ControllerBase
    {
        private readonly IDataProtector protection;
        private readonly ITimeLimitedDataProtector proteccionLimitadaPorTiempo;

        public SeguridadController(IDataProtectionProvider protectionProvider)
        {
            protection = protectionProvider.CreateProtector("SeguridadController");
            proteccionLimitadaPorTiempo = protection.ToTimeLimitedDataProtector();
        }

        [HttpGet("encriptar-por-tiempo")]
        public ActionResult EncriptarPorTiempo(string textoPlano)
        {
            string textoCifrado = proteccionLimitadaPorTiempo.Protect(textoPlano, lifetime: TimeSpan.FromSeconds(30));
            return Ok(new { textoCifrado });
        }

        [HttpGet("desencriptar-por-tiempo")]
        public ActionResult DesencriptarPorTiempo(string textoCifrado)
        {
            string textoPlano = proteccionLimitadaPorTiempo.Unprotect(textoCifrado);
            return Ok(new { textoPlano });
        }

        [HttpGet("encriptar")]
        public ActionResult Encriptar(string textoPlano)
        {
            string textoCifrado = protection.Protect(textoPlano);
            return Ok( new { textoCifrado });
        }

        [HttpGet("desencriptar")]
        public ActionResult Desencriptar(string textoCifrado)
        {
            string textoPlano = protection.Unprotect(textoCifrado);
            return Ok(new { textoPlano }); 
        }
    } 
}
