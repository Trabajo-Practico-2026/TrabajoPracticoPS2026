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
            catch (NotFoundException ex)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Recurso no encontrado.",
                    Details = ex.Message
                });
            }
            catch (ConcurrencyException ex)
            {
                context.Response.StatusCode = 409;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Conflicto de concurrencia.",
                    Details = ex.Message
                });
            }
            catch (ConflictException ex)
            {
                context.Response.StatusCode = 409;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Conflicto de estado.",
                    Details = ex.Message
                });
            }
            catch (DomainException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Ocurrió un error de dominio.",
                    Details = ex.Message
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    Message = "Ocurrió un error inesperado.",
                    Details = ex.Message
                });
            }

        }
    }
}
