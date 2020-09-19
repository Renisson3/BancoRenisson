using Envolva.Infra.CrossCutting.Jobs.Schedules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Envolva.Infra.CrossCutting.Jobs
{
    public static class JobSchedule
    {
        public static IApplicationBuilder UseJobSchedule(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            var scope = scopeFactory.CreateScope();
            var scopedContainer = scope.ServiceProvider;

            var calculateInterestSchedule = scopedContainer.GetRequiredService<CalculateInterestSchedule>();
            calculateInterestSchedule.ApplyJob();

            return app;
        }
    }
}