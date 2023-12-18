using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using AM.UI.View.Dialog;

namespace AM.UI.Command.RentalContract
{
    public class DeleteRentalContractCommand : AsyncCommandBase
    {
        private readonly RentalContractHomeVMUI _rentalvmui;
        private readonly RentalContractStore _Store;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;

        public DeleteRentalContractCommand(RentalContractHomeVMUI rentalvmui, RentalContractStore store, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _rentalvmui = rentalvmui;
            _Store = store;
            _navigator = navigator;
            _factory = factory;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _Store.DeleteRentalContract(_rentalvmui.SelectRentalContract.ID);
            if (result == true)
            {
                new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.RentalContract);
            }
            else
            {
                new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
