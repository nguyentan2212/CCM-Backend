using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DappAPI.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public List<TEntity> GetAll();
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        public List<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        public void Update(TEntity entity);
        public void UpdateRange(IEnumerable<TEntity> entities);
        public void Add(TEntity entity);
        public void AddRange(IEnumerable<TEntity> entities);
        public void Remove(TEntity entity);
    }
}
