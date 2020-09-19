using BancoRenisson.Infra.Data.Context;
using Envolva.Domain.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Envolva.Infra.Data.Writing.Repositories
{
    public abstract class BaseRepository<T>
        : IRepository<T>
        where T : class
    {
        protected readonly ContextMySql Context;
        private bool _skipSaveChanges;

        public BaseRepository(ContextMySql context)
        {
            Context = context;
            _skipSaveChanges = false;
        }

        public Task Add(T entity)
        {
            return AddRange(new[] { entity });
        }

        public Task AddRange(IEnumerable<T> entity)
        {
            Context.Set<T>().AddRange(entity);

            return SaveChanges();
        }

        public Task Remove(T entity)
        {
            Context.Set<T>().Remove(entity);

            return SaveChanges();
        }

        public Task RemoveRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);

            return SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            if (_skipSaveChanges) return 0;

            return await Context.SaveChangesAsync();
        }

        public void SkipSaveChanges(bool skip = true)
        {
            _skipSaveChanges = skip;
        }

        public Task Update(T entity)
        {
            Context.Set<T>().Update(entity);

            return SaveChanges();
        }

        public Task UpdateRange(IEnumerable<T> objs)
        {
            Context.Set<T>().UpdateRange(objs);

            return SaveChanges();
        }
    }
}