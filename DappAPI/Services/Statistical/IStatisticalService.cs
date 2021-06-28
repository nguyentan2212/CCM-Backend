using DappAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DappAPI.Services.Statistical
{
    public interface IStatisticalService
    {
        public StatisticalViewModel GetStastical();
    }
}
