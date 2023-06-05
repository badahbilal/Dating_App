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
app.MapControllers();

app.Run();
