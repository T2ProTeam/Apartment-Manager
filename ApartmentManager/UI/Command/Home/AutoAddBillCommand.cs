using AM.UI.State.Navigators;
using AM.UI.State;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Home;

namespace AM.UI.Command.Home
{
    public class AutoAddBillCommand : AsyncCommandBase
    {
        public readonly HomeBillListingViewModel _homeVM;
        public readonly HomeStore _homeStore;

        public AutoAddBillCommand(HomeBillListingViewModel homeVM, HomeStore homeStore)
        {
            _homeVM=homeVM;
            _homeStore=homeStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            AutoAddHomeVM autoAddHomeVM = new AutoAddHomeVM
            {
                date = _homeVM.now
            };
            var result = await _homeStore.AutoAddBill(autoAddHomeVM);
            if (result ==1)
            {
                _homeVM.LoadDatabase.Execute(null);
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
            else if (result==0)
                new MessageBoxCustom("Add Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            else if (result==-1) new MessageBoxCustom("Exist Bill Today", MessageType.Warning, MessageButtons.Ok).ShowDialog();
        }
    }
}