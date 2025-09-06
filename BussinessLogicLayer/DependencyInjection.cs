using BussinessLogicLayer.Mappers;
using BussinessLogicLayer.ServiceContracts;
using BussinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        // Add Business Logic Layer services into the IoC container

        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);

        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        services.AddScoped<IProductsService, eCommerce.BusinessLogicLayer.Services.ProductsService>();
        return services;
    }
}
   