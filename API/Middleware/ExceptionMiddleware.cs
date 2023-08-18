using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    // The 'ExceptionMiddleware' handles exceptions that occur during the processing of HTTP requests.
    // It logs the exception, constructs an error response, and sends it back to the client.

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this._env = env;
            this._logger = logger;
            this._next = next;
        }

        // The main method of the middleware that handles exceptions.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Execute the next middleware in the pipeline.
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception using the configured logger.
                _logger.LogError(ex, ex.Message);

                // Configure the HTTP response.
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Create an error response based on the environment (development or production).
                var response = _env.IsDevelopment()
                                ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                                : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

                // Serialize the error response to JSON.
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);

                // Write the JSON response to the HTTP response.
                await context.Response.WriteAsync(json);
            }
        }
    }

}