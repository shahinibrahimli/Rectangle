using Microsoft.Extensions.DependencyInjection;
using Rectangle.DomainServices.Contracts.RectangleServices;
using Rectangle.DomainServices.StepServices; 

namespace Rectangle.DomainServices;

public static class DomainServiceServiceRegistration
{
    public static IServiceCollection AddDomainServiceServices(this IServiceCollection services)
    {
        return services.AddScoped<IRectangleServices, RectangleServices>() 
            .AddHttpClient();
    }
}