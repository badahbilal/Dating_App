using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Middleware;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Here we call the Extension Method of Application Services
builder.Services.AddApplicationServices(builder.Configuration);
// Here we call the Extension Method of Identity Services
builder.Services.AddIdentityServices(builder.Configuration);


/*
Now we have two sections, if you like, inside this file.
Anything before app build app equals builder dot build is considered our services container.
Any services that we add, we add to this container.
*/
var app = builder.Build();

/*
And then the next part of this below this comment is what's referred to as the HTTP request pipeline.
*/
// Configure the HTTP request pipeline.


// Add the custom 'ExceptionMiddleware' to the request processing pipeline.
app.UseMiddleware<ExceptionMiddleware>();


// This code configures CORS middleware in the ASP.NET Core application pipeline.
// The app.UseCors() method is called with a configuration builder lambda expression.

// The configuration builder allows specifying the CORS policy for the application.
// In this case, the CORS policy allows requests from the "http://localhost:4200" origin,
// and it allows any HTTP method and any header.

// This configuration enables Cross-Origin Resource Sharing for the specified origin,
// allowing the application to receive requests from a different domain during development.
// It is important to configure CORS policies carefully in production environments to
// ensure appropriate security and access control.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));


// Enable authentication middleware to authenticate requests based on configured authentication schemes.
// This middleware validates the incoming requests for authentication information, such as JWT tokens.
app.UseAuthentication();


// Enable authorization middleware to perform authorization checks on authenticated requests.
// This middleware verifies whether the authenticated user has the necessary permissions to access the requested resource.
app.UseAuthorization();


app.MapControllers();


// In the application startup, create a service scope and perform database migration and seeding if required.
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    // Get the database context from the service provider.
    var context = services.GetRequiredService<DataContext>();

    // Apply pending database migrations asynchronously.
    await context.Database.MigrateAsync();

    // Seed initial user data into the database if no users exist.
    await Seed.SeedUsers(context);
}
catch (Exception ex)
{
    // If an exception occurs during migration or seeding, log the error.
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
}


app.Run();
