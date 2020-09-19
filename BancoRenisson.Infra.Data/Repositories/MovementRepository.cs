using BancoRenisson.Domain.Movimentacoes;
using BancoRenisson.Domain.Movimentacoes.Repositories;
using BancoRenisson.Infra.Data.Context;
using Envolva.Infra.Data.Writing.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoRenisson.Infra.Data.Repositories
{
    public class MovementRepository : BaseRepository<Movement>, IMovementRepository
    {
        public MovementRepository(ContextMySql context) : base(context)
        { }

        public async Task<Movement> SearchById(long movementId)
            => await Context.Movements
            .FirstOrDefaultAsync(t => t.Id.Equals(movementId));

        public async Task<IEnumerable<Movement>> GetAll()
            => await Context.Movements
            .Include(p => p.CurrentAccount)
            .ToListAsync();

        public async Task<IEnumerable<Movement>> ListMovementsByCurrentAccountId(long currentAccountId)
            => await Context.Movements
                .Where(t => t.CurrentAccountId.Equals(currentAccountId))
                .ToListAsync();
    }
}