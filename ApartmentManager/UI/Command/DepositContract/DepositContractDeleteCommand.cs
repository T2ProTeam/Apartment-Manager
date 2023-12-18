using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.DepositContract;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AM.UI.Command.DepositContract
{
    public class DepositContractDeleteCommand : AsyncCommandBase
    {
        private readonly DepositContractHomeVMUI _depositvmui;
        private readonly DepositContractStore _Store;

        public ICommand DepositNav { get; }

        public DepositContractDeleteCommand(DepositContractHomeVMUI depositvmui, DepositContractStore store,INavigator navigator,IAparmentViewModelFactory viewmodel)
        {
            _depositvmui = depositvmui;
            _Store = store;
            DepositNav = new UpdateCurrentViewModelCommand(navigator,viewmodel);
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _Store.DeleteDeposit(_depositvmui.SelectDeposits.ID);
            if (result == true)
            {
                new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                DepositNav.Execute(ViewType.DepositContract);
            }
            else
            {
                new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
    }
}
