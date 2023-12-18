using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.DepositContract;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.DepositsContract;

namespace AM.UI.Command.DepositContract
{
    public class DepositContractUpdateCommand : AsyncCommandBase
    {
        private readonly DepositContractUpdateVMUI _deposit;
        private readonly DepositContractStore _store;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewmodel;

        public ICommand DepositNav { get; }

        public DepositContractUpdateCommand(DepositContractUpdateVMUI deposit, DepositContractStore store,IAparmentViewModelFactory viewmodel,INavigator navigator)
        {
            _deposit = deposit;
            _store = store;
            _navigator = navigator;
            _viewmodel = viewmodel;
            DepositNav = new UpdateCurrentViewModelCommand(_navigator, _viewmodel);
            _deposit.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DepositsContractUpdateViewModel deposit = new DepositsContractUpdateViewModel
            {
                ID = _deposit.DepositViewmodel.ID,
                Room = _deposit.SelectRoom,
                DepositsDate = _deposit.DepositViewmodel.DepositsDate,
                ReceiveDate = _deposit.DepositViewmodel.ReceiveDate,
                CheckOutDate = _deposit.DepositViewmodel.CheckOutDate,
                Money =  _deposit.DepositViewmodel.Money
            };
            var result = await _store.UpdateDeposit(deposit);
            if (result != null)
            {
                new MessageBoxCustom("Update Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                DepositNav.Execute(ViewType.DepositContract);
            }
            else
            {
                new MessageBoxCustom("Update Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
