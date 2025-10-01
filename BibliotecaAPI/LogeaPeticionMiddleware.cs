using Microsoft.AspNetCore.Builder;

namespace BibliotecaAPI
{
    public class LogeaPeticionMiddleware
    {
        private readonly RequestDelegate next;

        public LogeaPeticionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext contexto)
        {
            //Cuando viene la peticion
            var logger = contexto.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"Peticion: {contexto.Request.Method} {contexto.Request.Path}");

            await next.Invoke(contexto);

            logger.LogInformation($"Respuesta: {contexto.Response.StatusCode}");
        }
    }

    public static class LogueaPeticionMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogueaPeticion(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogeaPeticionMiddleware>();
        }
    }
}
