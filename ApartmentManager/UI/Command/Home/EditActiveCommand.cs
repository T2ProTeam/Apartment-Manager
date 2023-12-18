using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using MaterialDesignThemes.Wpf;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Home;

namespace AM.UI.Command.Home
{
    public class EditActiveCommand : AsyncCommandBase
    {
        public readonly HomeBillListingViewModel _homeVM;
        public readonly HomeStore _Ihome;

        public EditActiveCommand(HomeBillListingViewModel homeVM, HomeStore homeStore)
        {
            _homeVM=homeVM;
            _Ihome=homeStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (_homeVM.selectItem!=null)
            {
                _homeVM.selectItem.IsActive = _homeVM.selectItem.IsActive;
                var bill = await _Ihome.EditActive(_homeVM.selectItem);
                if (bill != null)
                {
                    new MessageBoxCustom("Upadate Active Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else MessageBox.Show("fail");
            }
        }
    }
}