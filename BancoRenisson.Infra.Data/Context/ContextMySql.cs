using BancoRenisson.Domain.ContasCorrentes;
using BancoRenisson.Domain.Movimentacoes;
using BancoRenisson.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace BancoRenisson.Infra.Data.Context
{
    public class ContextMySql : DbContext
    {
        public ContextMySql(DbContextOptions options) : base(options)
        {
        }

        public DbSet<CurrentAccount> CurrentAccounts { get; set; }
        public DbSet<Movement> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CurrentAccount>().Map();
            modelBuilder.Entity<Movement>().Map();
        }
    }
}