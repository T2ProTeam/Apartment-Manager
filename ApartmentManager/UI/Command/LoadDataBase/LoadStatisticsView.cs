using AM.UI.State.Store;
using AM.UI.ViewModelUI.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Statistic;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadStatisticsView : AsyncCommandBase
    {
        private readonly StatisticsHomeVMUI _StatisticsVM;
        private readonly StatisticsStore _StatisticsStore;

        public LoadStatisticsView(StatisticsHomeVMUI statisticsVM, StatisticsStore statisticsStore)
        {
            _StatisticsVM = statisticsVM;
            _StatisticsStore = statisticsStore;
        }

        private StatisticTotalMoney totalTask = new StatisticTotalMoney();
        private MoneyOfGovernment governmentTask = new MoneyOfGovernment();
        private StatisticsProfitVm profitTask = new StatisticsProfitVm();

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                List<StatisticsVm> statistics = await _StatisticsStore.LoadStatistics((int)_StatisticsVM.MonthForStatistics, _StatisticsVM.SelectedYear);
                _StatisticsVM.UpdateDataStatistics(statistics);
                await Task.Run(CreateGovernment);
                await Task.WhenAll(TotalMoney(), GovernmentMoney(), ProfitMoney());
                _StatisticsVM.TotalProfitMoney = profitTask.ElectricMoney + profitTask.WaterMoney + profitTask.ServiceMoney;
            }
            catch (Exception)
            {
                _StatisticsVM.MessageError = "Error to load Statistics Data";
            }
        }

        public async Task TotalMoney()
        {
            totalTask = await _StatisticsStore.GetTotalMoney((int)_StatisticsVM.MonthForStatistics, _StatisticsVM.SelectedYear);
            _StatisticsVM.StatisticsTotal = totalTask;
        }

        public async Task GovernmentMoney()
        {
            governmentTask = await _StatisticsStore.GetMoneyOfGovernment((int)_StatisticsVM.MonthForStatistics, _StatisticsVM.SelectedYear);
            _StatisticsVM.GovernmentMoney = governmentTask;
        }

        public async Task ProfitMoney()
        {
            profitTask = await _StatisticsStore.GetProfitMoney((int)_StatisticsVM.MonthForStatistics, _StatisticsVM.SelectedYear);
            _StatisticsVM.StatisticsProfitVM = profitTask;
        }

        public async Task CreateGovernment()
        {
            await _StatisticsStore.CreateGovernment((int)_StatisticsVM.MonthForStatistics, _StatisticsVM.SelectedYear);
        }
    }
}