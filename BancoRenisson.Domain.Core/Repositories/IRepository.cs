using System.Collections.Generic;
using System.Threading.Tasks;

namespace Envolva.Domain.Core.Repositories
{
    public interface IRepository<T> where T : class
    {
        void SkipSaveChanges(bool skip = true);

        Task<int> SaveChanges();

        Task Add(T entity);

        Task AddRange(IEnumerable<T> entities);

        Task Update(T entity);

        Task UpdateRange(IEnumerable<T> entities);

        Task Remove(T entity);

        Task RemoveRange(IEnumerable<T> entities);
    }
}