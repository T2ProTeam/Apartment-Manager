using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Bills;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.Bill
{
    public class BillDeleteCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModel;
        private readonly BillStore _store;
        private readonly BillHomeVMUI _billvm;

        public BillDeleteCommand(INavigator navigator, IAparmentViewModelFactory viewModel, BillStore store, BillHomeVMUI billvm)
        {
            _navigator = navigator;
            _viewModel = viewModel;
            _store = store;
            _billvm = billvm;
            _billvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _store.DeleteBill(_billvm.SelectBill.ID);
            if (result == true)
            {
                new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _viewModel.CreateViewModel(ViewType.Bill);
            }
            else
            {
                new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}