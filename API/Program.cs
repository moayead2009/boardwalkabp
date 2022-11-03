using Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add postgres entity provider
builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("ApbDbConnection")));

// For InvalidCastException
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors(options =>
            options.WithOrigins("http://localhost:3000").
            AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
