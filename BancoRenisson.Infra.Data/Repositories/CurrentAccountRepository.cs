using BancoRenisson.Domain.ContasCorrentes;
using BancoRenisson.Domain.ContasCorrentes.Repositories;
using BancoRenisson.Infra.Data.Context;
using Envolva.Infra.Data.Writing.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoRenisson.Infra.Data.Repositories
{
    public class CurrentAccountRepository : BaseRepository<CurrentAccount>, ICurrentAccountRepository
    {
        public CurrentAccountRepository(ContextMySql context) : base(context)
        { }

        public async Task<CurrentAccount> SearchById(Guid currentId)
            => await Context.CurrentAccounts
            .FirstOrDefaultAsync(t => t.Id.Equals(currentId));

        public async Task<CurrentAccount> SearchByNumber(int currentNumber)
            => await Context.CurrentAccounts
            .FirstOrDefaultAsync(t => t.NumberAccount == currentNumber);

        public async Task<IEnumerable<CurrentAccount>> GetAll()
            => await Context.CurrentAccounts.ToListAsync();

        public async Task<IEnumerable<CurrentAccount>> GetAllCalculateInterest()
        {
            return await Context.CurrentAccounts
                       .Where(p => p.Value > 0)
                       .ToListAsync();
        }
    }
}