using System;
using ecommerceApi.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ecommerceApi.DAL;

public static class Extentions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EcommerceContext>(options =>
        {
            var connectionString = configuration["ConnectionString"];

            options.UseNpgsql(connectionString);
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddHostedService<DatabaseInitializer<EcommerceContext>>();

        return services;
    }
}
