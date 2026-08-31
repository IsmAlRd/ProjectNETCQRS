using FluentValidation;
using Reservations.Domain.Exceptions;
using System.Text.Json;

namespace Reservations.API.Middleware;

public class ErrorHandlingMiddleware
{
  private readonly RequestDelegate _next;

  public ErrorHandlingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (DomainException ex)
    {
      context.Response.StatusCode = StatusCodes.Status400BadRequest;
      context.Response.ContentType = "application/json";
      await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = ex.Message }));
    }
    catch (ValidationException ex)
    {
      context.Response.StatusCode = StatusCodes.Status400BadRequest;
      context.Response.ContentType = "application/json";
      var errors = ex.Errors.Select(e => new { field = e.PropertyName, error = e.ErrorMessage });
      await context.Response.WriteAsync(JsonSerializer.Serialize(new { errors }));
    }
    catch (Exception)
    {
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;
      context.Response.ContentType = "application/json";
      await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Erro interno do servidor." }));
    }
  }
}