using BancoRenisson.Domain.ContasCorrentes.Repositories;
using BancoRenisson.Domain.Movimentacoes.Repositories;
using BancoRenisson.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BancoRenisson.Infra.CrossCutting_Ioc.Modules
{
    public static class RepositoriesConfigurationExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMovementRepository, MovementRepository>();
            services.AddTransient<ICurrentAccountRepository, CurrentAccountRepository>();

            return services;
        }
    }
}