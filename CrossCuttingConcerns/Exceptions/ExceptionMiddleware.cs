using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;

namespace CrossCuttingConcerns.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
           await HandleException(context,exception);
        }
    }

    private Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;

        if (exception.GetType() == typeof(ValidationException))
        {
            return CreateValidationException(context, exception);
        }

        throw new Exception("Diğer hatalar...");

    }

    private Task CreateValidationException(HttpContext context, Exception exception)
    {

        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
        object errors = ((ValidationException)exception).Errors;

        ValidationProblemDetails problemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://example.com/probs/validation",
            Title = "Validation error(s)",
            Detail = "",
            Instance = "",
            Errors = errors
        };

        var result = JsonConvert.SerializeObject(problemDetails);

        return context.Response.WriteAsync(result);

    }
}