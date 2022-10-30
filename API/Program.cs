using ApplicationBuildingPlatformAPI.Models.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using ApplicationBuildingPlatformAPI.Repositories.Contracts;
using ApplicationBuildingPlatformAPI.Repositories.Domain;
using ApplicationBuildingPlatformAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// For Entity Framework  
builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("ApbDbConnection")));

// For InvalidCastException
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
.AddJwtBearer(options =>
 {
     options.SaveToken = true;
     options.RequireHttpsMetadata = false;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidAudience = builder.Configuration["JWT:ValidAudience"],
         ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
     };
 });

builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors(options =>
            options.WithOrigins("http://localhost:3000").
            AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
