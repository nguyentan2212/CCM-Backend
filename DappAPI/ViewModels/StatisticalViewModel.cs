using System.Collections.Generic;

namespace DappAPI.ViewModels
{
    public class StatisticalViewModel
    {
        public long TotalCapital { get; set; }
        public long TotalEquity { get; set; }
        public long TotalWorking { get; set; }
        public long TotalShortTermAsset { get; set; }
        public long TotalLongTermAsset { get; set; }
        public long TotalUser { get; set; }
        public List<TopUserViewModel> TopUserList { get; set; }
        public List<long> CapitalLineChartDataSet { get; set; }
        public List<string> CapitalLineChartLabels { get; set; }

    }
}
