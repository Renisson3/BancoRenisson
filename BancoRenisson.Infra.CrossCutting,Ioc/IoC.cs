using Envolva.Infra.CrossCutting.Ioc.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Envolva.Infra.CrossCutting.Ioc
{
    public static class IoC
    {
        public static IServiceCollection AddCoreModules(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddCoreOptions(configuration).AddInfrastructureModules();

            return services;
        }
    }
}