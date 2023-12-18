using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Bills;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Bill;

namespace AM.UI.Command.Bill
{
    public class BillUpdateCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModel;
        private readonly BillStore _store;
        private readonly BillUpdateVMUI _billvm;

        public BillUpdateCommand(INavigator navigator, IAparmentViewModelFactory viewModel, BillStore store, BillUpdateVMUI billvm)
        {
            _navigator = navigator;
            _viewModel = viewModel;
            _store = store;
            _billvm = billvm;
            _billvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            BillUpdateViewModel update = new BillUpdateViewModel
            {
                ID = _billvm.BillVm.ID,
                Rental = _billvm.SelectRental,
                Active = _billvm.BillVm.Active,
                PayDate = _billvm.BillVm.PayDate,
                TotalMoney = _billvm.BillVm.TotalMoney
            };
            var result = await _store.UpdateBill(update);
            if (result != null)
            {
                new MessageBoxCustom("Update Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _viewModel.CreateViewModel(ViewType.Bill);
            }
            else
            {
                new MessageBoxCustom("Update Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
