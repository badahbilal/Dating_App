using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
        {
            // This block of code registers the DataContext class as a service in the dependency injection container, using the AddDbContext method.
            // The AddDbContext method configures the DataContext to use SQLite as the database provider, using the connection string retrieved from the application configuration.
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("SqliteDatingAppConnection"));
            });


            // This code adds Cross-Origin Resource Sharing (CORS) support to the ASP.NET Core application.
            // The AddCors() method is called on the IServiceCollection to configure CORS settings.
            // CORS allows controlled access to resources on a different domain, which is typically restricted by browsers
            // due to the same-origin policy. By enabling CORS, the server can specify which origins are allowed to access
            // its resources and which HTTP methods and headers are permitted.
            // By calling AddCors() without any additional configuration, the default CORS policy is applied, which allows
            // all origins, all methods, and all headers. This is suitable for development purposes, but it is recommended
            // to configure CORS with more specific rules in production environments.
            services.AddCors();

            // Register the TokenService implementation as a scoped service for the ITokenService interface.
            // The TokenService is responsible for generating and managing authentication tokens.
            services.AddScoped<ITokenService, TokenService>();


            // Register the 'IUserRepository' service with the 'UserRepository' implementation.
            services.AddScoped<IUserRepository, UserRepository>();

            // Configure AutoMapper and register it with the application's services.
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            return services;
        }
    }
}