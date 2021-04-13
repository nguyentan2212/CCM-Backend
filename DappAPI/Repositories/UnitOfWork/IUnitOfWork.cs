using System;
using System.Threading.Tasks;

namespace DappAPI.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public Repository<TEntity> CreateRepository<TEntity>() where TEntity : class;
        public Task SaveAsync();
    }
}
