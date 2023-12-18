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
using System.Windows;

namespace AM.UI.Command.Statistics
{
    public class CreateGovernmentCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;

        private readonly IAparmentViewModelFactory _Factory;
        private readonly StatisticsStore _Store;
        private readonly StatisticsHomeVMUI _StatisticsVM;

        public CreateGovernmentCommand(INavigator navigator, IAparmentViewModelFactory Factory, StatisticsStore Store, StatisticsHomeVMUI StatisticsVM)
        {
            _navigator = navigator;
            _Factory = Factory;
            _Store = Store;
            _StatisticsVM = StatisticsVM;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _Store.CreateGovernment((int)_StatisticsVM.MonthForStatistics, _StatisticsVM.SelectedYear);
            if (result != null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _Factory.CreateViewModel(ViewType.Statistics);
            }
            else
            {
                new MessageBoxCustom("Fail", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}