using System.Net;
using System.Text.Json;
using LibraryAPI.Exceptions;


public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;


    public ExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode =
                (int)HttpStatusCode.NotFound;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = ex.Message
                });
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode =
              (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = ex.Message
                });
        }
        catch (ConflictException ex)
        {
            context.Response.StatusCode =
           (int)HttpStatusCode.Conflict;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = ex.Message
                });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode =
                500;

            await context.Response.WriteAsJsonAsync(
                new
                {
                    message = "Internal server error"
                });
        }
    }
}