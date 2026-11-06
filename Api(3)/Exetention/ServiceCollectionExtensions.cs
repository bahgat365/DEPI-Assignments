using DEPI_ECommerce_API.Data;
using DEPI_ECommerce_API.Data.Interceptors;
using DEPI_ECommerce_API.Repositories;
using DEPI_ECommerce_API.Repositories.Interfaces;
using DEPI_ECommerce_API.Services;
using DEPI_ECommerce_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DEPI_ECommerce_API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddECommerceServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<CommandLoggingInterceptor>();

        services.AddDbContext<ECommerceDbContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            options.AddInterceptors(serviceProvider.GetRequiredService<CommandLoggingInterceptor>());
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));

        return services;
    }
}
