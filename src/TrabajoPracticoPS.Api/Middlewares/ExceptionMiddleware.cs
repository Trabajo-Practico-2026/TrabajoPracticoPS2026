using TrabajoPracticoPS.Domain.Exceptions;

namespace TrabajoPracticoPS.Api.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (DomainException ex)
            {
                context.Response.StatusCode = 400; // Bad Request
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Message = "Ocurrió un error de dominio.",
                    Details = ex.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500; // Internal Server Error
                context.Response.ContentType = "application/json";
                var response = new
                {
                    Message = "Ocurrió un error inesperado.",
                    Details = ex.Message
                };
                await context.Response.WriteAsJsonAsync(response);
            }
            
        }
    }
}
