using AM.UI.State.Navigators;
using AM.UI.State.Store;
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
    public class BillAddCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModel;
        private readonly BillStore _store;
        private readonly BillAddVMUI _billvm;

        public BillAddCommand(INavigator navigator, IAparmentViewModelFactory viewModel, BillStore store, BillAddVMUI billvm)
        {
            _navigator = navigator;
            _viewModel = viewModel;
            _store = store;
            _billvm = billvm;
            _billvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            BillCreateViewModel create = new BillCreateViewModel
            {
                Rental = _billvm.SelectRental,
                Active = _billvm.Active,
                ElectricQuantity = _billvm.ElectricQuantity,
                PayDate = _billvm.PayDate,
            };
            var result = await _store.AddBill(create);
            if (result != null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _viewModel.CreateViewModel(ViewType.Bill);
            }
            else
            {
                new MessageBoxCustom("Fail", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}