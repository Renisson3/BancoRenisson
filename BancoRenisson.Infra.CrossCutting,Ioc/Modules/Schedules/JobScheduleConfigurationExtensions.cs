using Envolva.Infra.CrossCutting.Jobs.Schedules;
using Microsoft.Extensions.DependencyInjection;

namespace Envolva.Infra.CrossCutting.Ioc.Modules.Schedules
{
    public static class JobScheduleConfigurationExtensions
    {
        public static IServiceCollection AddJobSchedule(this IServiceCollection services)
        {
            services.AddTransient<CalculateInterestSchedule>();

            return services;
        }
    }
}