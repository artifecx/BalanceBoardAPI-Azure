using Microsoft.Extensions.DependencyInjection;
using Application.Common.Mediator;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator();
            return services;
        }
    }
}
