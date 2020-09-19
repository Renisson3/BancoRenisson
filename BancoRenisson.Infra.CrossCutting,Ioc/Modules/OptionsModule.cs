using BancoRenisson.Infra.Data.Context;
using Envolva.Infra.CrossCutting.Jobs.Schedules.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Envolva.Infra.CrossCutting.Ioc.Modules
{
    public static class OptionsModule
    {
        public static IServiceCollection AddCoreOptions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<JobScheduleOptionsDiary>(configuration.GetSection("JobScheduleDiary"));
            services.Configure<ContextOptions>(configuration.GetSection("ConnectionStrings"));

            return services;
        }
    }
}