using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Statistic;

namespace Services.Interface
{
    public interface IStatistics
    {
        Task<PagedResult<StatisticsVm>> GetAllPage(RequestPaging request, int Month, int Year);

        Task<StatisticTotalMoney> GetElectricAndWater(int Month, int Year);

        Task<MoneyOfGovernment> GetGovermentMoney(int Month, int Year);

        Task<Statistics> CreateGovermentMoney(int Month, int Year);

        Task<Statistics> EditGovernmentMoney(EditGorvernmentMoney edit);

        Task<StatisticsProfitVm> GetProfitMoney(int Month, int Year);
    }
}