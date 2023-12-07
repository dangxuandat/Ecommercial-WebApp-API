using ApplicationCore;
using ApplicationCore.Implements;
using ECommercialAPI.Constants;
using Infrastructure.Models;

namespace ECommercialAPI.Extensions;
using Microsoft.EntityFrameworkCore;
public static class Extensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddDbContext<EcommercialContext>();
        services.AddScoped<IUnitOfWork<EcommercialContext>, UnitOfWork<EcommercialContext>>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables(EnvironmentVariableConstant.Prefix).Build();
        services.AddDbContext<EcommercialContext>(
            options => options.UseSqlServer(CreateConnectionString(configuration)));
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