using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Statistic;

namespace AM.UI.Command.Statistics
{
    public class EditGovernmentCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;

        private readonly IAparmentViewModelFactory _Factory;
        private readonly StatisticsStore _Store;
        private readonly StatisticsHomeVMUI _StatisticsVMUI;

        public EditGovernmentCommand(INavigator navigator, IAparmentViewModelFactory Factory, StatisticsStore Store, StatisticsHomeVMUI StatisticsVMUI)
        {
            _navigator = navigator;
            _Factory = Factory;
            _Store = Store;
            _StatisticsVMUI = StatisticsVMUI;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            EditGorvernmentMoney edit = new EditGorvernmentMoney
            {
                ID = _StatisticsVMUI.GovernmentMoney.ID,
                ElectricMoneyOfGovernment = _StatisticsVMUI.GovernmentMoney.ElectricMoneyOfGovernment,
                WaterMoneyOfGovernment = _StatisticsVMUI.GovernmentMoney.WaterMoneyOfGovernment,
                ServiceOfGovernment = _StatisticsVMUI.GovernmentMoney.ServiceOfGovernment,
                Month = (int)_StatisticsVMUI.MonthForStatistics,
                Year = (int)_StatisticsVMUI.SelectedYear
            };
            var result = await _Store.EditGovernment(edit);
            if (result != null)
            {
                new MessageBoxCustom("Update Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentHomeViewModel = _Factory.CreateViewModel(ViewType.Statistics);
            }
            else
            {
                new MessageBoxCustom("Update Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}