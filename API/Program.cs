using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// This block of code registers the DataContext class as a service in the dependency injection container, using the AddDbContext method.
// The AddDbContext method configures the DataContext to use SQLite as the database provider, using the connection string retrieved from the application configuration.
builder.Services.AddDbContext<DataContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatingAppConnection"));
});


// This code adds Cross-Origin Resource Sharing (CORS) support to the ASP.NET Core application.
// The AddCors() method is called on the IServiceCollection to configure CORS settings.
// CORS allows controlled access to resources on a different domain, which is typically restricted by browsers
// due to the same-origin policy. By enabling CORS, the server can specify which origins are allowed to access
// its resources and which HTTP methods and headers are permitted.
// By calling AddCors() without any additional configuration, the default CORS policy is applied, which allows
// all origins, all methods, and all headers. This is suitable for development purposes, but it is recommended
// to configure CORS with more specific rules in production environments.
builder.Services.AddCors();


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


// This code configures CORS middleware in the ASP.NET Core application pipeline.
// The app.UseCors() method is called with a configuration builder lambda expression.

// The configuration builder allows specifying the CORS policy for the application.
// In this case, the CORS policy allows requests from the "http://localhost:4200" origin,
// and it allows any HTTP method and any header.

// This configuration enables Cross-Origin Resource Sharing for the specified origin,
// allowing the application to receive requests from a different domain during development.
// It is important to configure CORS policies carefully in production environments to
// ensure appropriate security and access control.

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));


app.MapControllers();

app.Run();
