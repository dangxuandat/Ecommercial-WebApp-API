using System.Text;
using ApplicationCore;
using ApplicationCore.Implements;
using ECommercialAPI.Constants;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ECommercialAPI.Extensions;
using Microsoft.EntityFrameworkCore;
public static class Extensions
{
    private static readonly IConfiguration Configuration = new ConfigurationBuilder()
        .AddEnvironmentVariables(EnvironmentVariableConstant.Prefix).Build();
    
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddDbContext<EcommercialContext>();
        services.AddScoped<IUnitOfWork<EcommercialContext>, UnitOfWork<EcommercialContext>>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<EcommercialContext>(
            options => options.UseSqlServer(CreateConnectionString(Configuration)));
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = Configuration[JwtConstant.Issuer],
                ValidAudience = Configuration[JwtConstant.Audience],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[JwtConstant.SecretKey])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
            };
        });
        return services;
    }
    private static string CreateConnectionString(IConfiguration configuration)
    {
        string connectionString =
            $"Server={configuration[DatabaseConstant.Host]},{configuration[DatabaseConstant.Port]};" +
            $"User Id={configuration[DatabaseConstant.UserName]},Password={configuration[DatabaseConstant.Password]};" +
            $"Database={configuration[DatabaseConstant.Database]}";
        return connectionString;
    }
}