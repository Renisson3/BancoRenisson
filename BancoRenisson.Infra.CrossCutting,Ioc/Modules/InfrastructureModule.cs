using BancoRenisson.Infra.CrossCutting_Ioc.Modules;
using Envolva.Infra.CrossCutting.Ioc.Modules.Schedules;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Envolva.Infra.CrossCutting.Ioc.Modules
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructureModules(this IServiceCollection services)
        {
            var inMemory = GlobalConfiguration.Configuration.UseMemoryStorage();

            services.AddRepositories();
            services.AddJobSchedule();
            services.AddHangfire(x => x.UseStorage(inMemory));

            return services;
        }
    }
}