using Envolva.Domain.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoRenisson.Domain.ContasCorrentes.Repositories
{
    public interface ICurrentAccountRepository : IRepository<CurrentAccount>
    {
        Task<CurrentAccount> SearchById(Guid currentId);

        Task<CurrentAccount> SearchByNumber(int currentNumber);

        Task<IEnumerable<CurrentAccount>> GetAll();

        Task<IEnumerable<CurrentAccount>> GetAllCalculateInterest();
    }
}