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
    public class DepositContractAddCommand : AsyncCommandBase
    {
        private readonly DepositContractStore _Store;
        private readonly DepositContractAddVMUI _depositvm;
        public ICommand DepositNav { get; }

        public DepositContractAddCommand(DepositContractStore store, DepositContractAddVMUI depositvm, INavigator navigator, IAparmentViewModelFactory viewModel)
        {
            _Store = store;
            _depositvm = depositvm;
            _depositvm.PropertyChanged += OnViewModelPropertyChanged;
            DepositNav = new UpdateCurrentViewModelCommand(navigator, viewModel);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            DepositsContractCreateViewModel create = new DepositsContractCreateViewModel
            {
                Room = _depositvm.SelectRoom,
                Customer = _depositvm.SelectCustomer,
                DepositsDate = _depositvm.DepositDate,
                ReceiveDate = _depositvm.ReceiveDate,
                CheckOutDate = _depositvm.CheckOutDate,
                Money = _depositvm.Money
            };
            var result = await _Store.CreateDeposit(create);
            if (result != null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                DepositNav.Execute(ViewType.DepositContract);
            }
            else
            {
                new MessageBoxCustom("Fail", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
        }

        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}