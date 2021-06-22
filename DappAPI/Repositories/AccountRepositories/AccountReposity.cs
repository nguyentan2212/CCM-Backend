using DappAPI.Contexts;
using DappAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DappAPI.Repositories.AccountRepositories
{
    public class AccountReposity : Repository<DappUser>, IAccountRepository
    {
        public AccountReposity(DappContext context) : base(context)
        {

        }

        public AccountReposity(Repository<DappUser> repository) : base(repository)
        {

        }

        public override List<DappUser> GetAll()
        {
            return dbSet.Include(y => y.CreatedCapitals).ToList();
        }

        public override List<DappUser> Get(Expression<Func<DappUser, bool>> predicate)
        {
            return dbSet.Include(y => y.CreatedCapitals).Where(predicate).ToList();
        }

        public override DappUser FirstOrDefault(Expression<Func<DappUser, bool>> predicate)
        {
            return dbSet.Include(y => y.CreatedCapitals).FirstOrDefault(predicate);
        }
    }
}
