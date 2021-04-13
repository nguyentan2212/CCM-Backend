using DappAPI.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DappAPI.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region variables
        private readonly DappContext context;
        private bool disposed = false;
        private Dictionary<string, object> repositories;
        #endregion

        public UnitOfWork(DappContext context)
        {
            this.context = context;
        }

        public Repository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<string, object>();
            }
            string type = typeof(TEntity).Name;
            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<TEntity>);
                var repositoryInstance = Activator.CreateInstance(repositoryType, context);
                repositories.Add(type, repositoryInstance);
            }
            return (Repository<TEntity>)repositories[type];
        }
       

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
