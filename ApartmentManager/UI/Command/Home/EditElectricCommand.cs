using AM.UI.State.Store;
using AM.UI.View.ConfirmForm;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Home;
using ViewModel.People;

namespace AM.UI.Command.Home
{
    public class EditElectricCommand : AsyncCommandBase
    {
        private readonly HomeBillListingViewModel _homevm;
        private readonly HomeStore _Ihome;

        public EditElectricCommand(HomeBillListingViewModel homevm, HomeStore ihome)
        {
            _homevm=homevm;
            _Ihome=ihome;
            _homevm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            if (parameter is UpdateElectricQuanlity model)
            {
                var result = await _Ihome.UpdateElectric(model);
                if (result!=null)
                {
                    _homevm.LoadDatabase.Execute(null);
                    new MessageBoxCustom("Edit Electic Quanlity Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
                else { new MessageBoxCustom("Edit Electic Quanlity Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog(); }
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}