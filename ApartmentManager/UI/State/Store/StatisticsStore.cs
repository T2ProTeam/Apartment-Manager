using AM.UI.Model;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Bill;
using ViewModel.Furniture;
using ViewModel.Statistic;

namespace AM.UI.State.Store
{
    public class StatisticsStore
    {
        private readonly Apartment _Apartment;

        private readonly IStatistics _IStatistics;

        private Lazy<Task> _InitializeStatistics;

        private List<BillVm> _billVM;
        public List<BillVm> BillVM => _billVM;

        private List<StatisticsVm> _StatisticsVM;
        public List<StatisticsVm> StatisticsVM => _StatisticsVM;

        private List<StatisticsProfitVm> _StatisticsProfitVM;
        public List<StatisticsProfitVm> StatisticsProfitVM => _StatisticsProfitVM;

        private MoneyOfGovernment _GovernmentVM;

        public MoneyOfGovernment GovernmentVM => _GovernmentVM;

        // public event Action<StatisticsProfitVm> CreateStatistic;

        public event Action<MoneyOfGovernment> EditGovernmentMoney;

        public StatisticsStore(Apartment Apartment, IStatistics IStatistics)
        {
            _Apartment = Apartment;
            _StatisticsVM = new List<StatisticsVm>();
            _IStatistics = IStatistics;
            _GovernmentVM = new MoneyOfGovernment();
        }

        public async Task<List<StatisticsVm>> LoadStatistics(int Month, int Year)
        {
            List<StatisticsVm> statistics = await _Apartment.GetAllStaticstis(Month, Year);
            return statistics;
        }

        public async Task<Statistics> EditGovernment(EditGorvernmentMoney edit)
        {
            var result = await _IStatistics.EditGovernmentMoney(edit);
            MoneyOfGovernment government = new MoneyOfGovernment
            {
                ElectricMoneyOfGovernment = edit.ElectricMoneyOfGovernment,
                WaterMoneyOfGovernment = edit.WaterMoneyOfGovernment,
                ServiceOfGovernment = edit.ServiceOfGovernment,
            };
            _GovernmentVM.ElectricMoneyOfGovernment = government.ElectricMoneyOfGovernment;
            _GovernmentVM.WaterMoneyOfGovernment = government.WaterMoneyOfGovernment;
            _GovernmentVM.ServiceOfGovernment = government.ServiceOfGovernment;
            EditGovernmentMoney.Invoke(government);
            return result;
        }

        public async Task<Statistics> CreateGovernment(int Month, int Year)
        {
            return await _IStatistics.CreateGovermentMoney(Month, Year);
        }

        public async Task<StatisticTotalMoney> GetTotalMoney(int Month, int Year)
        {
            return await _IStatistics.GetElectricAndWater(Month, Year);
        }

        public async Task<MoneyOfGovernment> GetMoneyOfGovernment(int Month, int Year)
        {
            return await _IStatistics.GetGovermentMoney(Month, Year);
        }

        public async Task<StatisticsProfitVm> GetProfitMoney(int Month, int Year)
        {
            return await _IStatistics.GetProfitMoney(Month, Year);
        }
    }
}