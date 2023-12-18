using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.Home
{
    public class LoadBillListingCommand : AsyncCommandBase
    {
        private readonly HomeBillListingViewModel _viewmodel;
        private readonly HomeStore _homestore;

        public LoadBillListingCommand(HomeBillListingViewModel viewmodel, HomeStore homestore)
        {
            _viewmodel=viewmodel;
            _homestore=homestore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewmodel.IsLoading =true;
            try
            {
                await _homestore.LoadBill(_viewmodel.now);

                _viewmodel.LoadHomeVM(_homestore.HomeItemVMs);
            }
            catch (Exception)
            {
            }
            _viewmodel.IsLoading = false;
        }
    }
}