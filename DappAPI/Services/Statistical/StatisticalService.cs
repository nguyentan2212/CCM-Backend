using DappAPI.Extensions.Enums;
using DappAPI.Models;
using DappAPI.Services.Account;
using DappAPI.Services.CapitalServices;
using DappAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DappAPI.Services.Statistical
{
    public class StatisticalService : IStatisticalService
    {
        private readonly IAccountService accountService;
        private readonly ICapitalService capitalService;
        public StatisticalService(IAccountService accountService, ICapitalService capitalService)
        {
            this.accountService = accountService;
            this.capitalService = capitalService;
        }

        public StatisticalViewModel GetStastical()
        {
            List<CapitalDataViewModel> capitals = capitalService.GetAllCapitals().Where(x => x.Status == "Finished").ToList();
            List<UserDataViewModel> users = accountService.GetAllUsersInfo();
            StatisticalViewModel statistical = new StatisticalViewModel();
            statistical.TotalCapital = capitals.Sum(x => x.Value);
            statistical.TotalEquity = capitals.Where(x => x.Type == "Equity").Sum(x => x.Value);
            statistical.TotalWorking = capitals.Where(x => x.Type == "Working").Sum(x => x.Value);
            statistical.TotalLongTermAsset = capitals.Where(x => x.Asset == "LongTermAsset").Sum(x => x.Value);
            statistical.TotalShortTermAsset = capitals.Where(x => x.Asset == "ShortTermAsset").Sum(x => x.Value);
            statistical.CapitalLineChartDataSet = new List<long>();
            statistical.CapitalLineChartLabels = new List<string>();
            for (int i = 11; i >= 0; i--)
            {
                DateTime temp = DateTime.Today.AddMonths(-i);
                statistical.CapitalLineChartLabels.Add(temp.ToString("MMM"));

                long tempSum = capitals.Where(capital => capital.CreationDate.Month == temp.Month && capital.CreationDate.Year == temp.Year)
                                         .Sum(x => x.Value);
                statistical.CapitalLineChartDataSet.Add(tempSum);
            }
            statistical.TotalUser = users.Count();
            statistical.TopUserList = accountService.GetTopUsers(5);
            return statistical;
        }
    }
}
