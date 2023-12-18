using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.RentalContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.RentalContract
{
    public class AddCustomerInRentalCommand : AsyncCommandBase
    {
        private readonly RentalContractStore store;
        private readonly AddCustomerInRentalVMUI _vm;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;

        public AddCustomerInRentalCommand(RentalContractStore store, AddCustomerInRentalVMUI vm, INavigator navigator, IAparmentViewModelFactory factory)
        {
            this.store = store;
            _vm = vm;
            _navigator = navigator;
            _factory = factory;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await store.CreateManyCustomer(_vm.peoples.ToList());
            if (result != null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.RentalContract);
            }
            else
                new MessageBoxCustom("Add Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
        }
    }
}