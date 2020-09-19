using Envolva.Domain.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BancoRenisson.Domain.Movimentacoes.Repositories
{
    public interface IMovementRepository : IRepository<Movement>
    {
        Task<Movement> SearchById(long movementId);

        Task<IEnumerable<Movement>> GetAll();

        Task<IEnumerable<Movement>> ListMovementsByCurrentAccountId(long currentAccountId);
    }
}