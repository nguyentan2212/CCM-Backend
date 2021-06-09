using DappAPI.Contexts;
using DappAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DappAPI.Repositories.CapitalRepositories
{
    public class CapitalRepository : Repository<Capital>, ICapitalRepository
    {
        public CapitalRepository(DappContext context) : base(context)
        {

        }

        public CapitalRepository(Repository<Capital> repository) : base(repository)
        {
            
        }

        public override List<Capital> GetAll()
        {
            return dbSet.Include(x => x.Approver).Include(y => y.Creator).ToList();
        }

        public override List<Capital> Get(Expression<Func<Capital, bool>> predicate)
        {
            return dbSet.Include(x => x.Approver).Include(y => y.Creator).Where(predicate).ToList();
        }

        public override Capital FirstOrDefault(Expression<Func<Capital, bool>> predicate)
        {
            return dbSet.Include(x => x.Approver).Include(y => y.Creator).FirstOrDefault(predicate);
        }
    }
}
