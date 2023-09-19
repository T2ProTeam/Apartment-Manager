﻿using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.Home
{
    public class LoadCustomerListtingCommand : AsyncCommandBase
    {
        private readonly HomeViewDetailCustomerVMUI _viewmodel;
        private readonly HomeStore _homestore;

        public LoadCustomerListtingCommand(HomeViewDetailCustomerVMUI viewmodel, HomeStore homestore)
        {
            _viewmodel=viewmodel;
            _homestore=homestore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewmodel.IsLoading =true;
            try
            {
                await _homestore.LoadCustomer();
                _viewmodel.UpdateData(_homestore.HomeCustomerListingVM);
            }
            catch (Exception)
            {
            }
            _viewmodel.IsLoading = false;
        }
    }
}