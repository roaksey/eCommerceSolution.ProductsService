using BussinessLogicLayer.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        // Add Business Logic Layer services into the IoC container

        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        return services;
    }
}
   