using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.Home
{
    public class DeletePaymentCommand : AsyncCommandBase
    {
        private readonly HomeStore _store;
        private readonly HomePaymentListtingVMUI _viewmodel;
        private readonly INavigator _navigator;
        public readonly IAparmentViewModelFactory _viewModelFactory;

        public DeletePaymentCommand(HomeStore store, HomePaymentListtingVMUI viewmodel, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _store=store;
            _viewmodel=viewmodel;
            _navigator=navigator;
            _viewModelFactory=factory;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_viewmodel.SelectedPE !=null)
            {
                var result = await _store.DeletePayment(_viewmodel.SelectedPE.ID, _viewmodel.SelectedPE.IDBill);
                if (result ==1)
                {
                    // _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Home);
                    new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else
                {
                    new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                }
            }
        }
    }
}