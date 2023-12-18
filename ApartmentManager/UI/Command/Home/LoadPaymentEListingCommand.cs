using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AM.UI.Command.Home
{
    public class LoadPaymentEListingCommand : AsyncCommandBase
    {
        private readonly HomePaymentListtingVMUI _viewmodel;
        private readonly HomeStore _homestore;

        public LoadPaymentEListingCommand(HomePaymentListtingVMUI viewmodel, HomeStore homestore)
        {
            _viewmodel=viewmodel;
            _homestore=homestore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewmodel.IsLoading =true;
            try
            {
                await _homestore.LoadPayment();
                _viewmodel.UpdateData(_homestore.HomePEVM);
            }
            catch (Exception)
            {
            }
            _viewmodel.IsLoading = false;
        }
    }
}