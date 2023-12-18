using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.RoomDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command.RoomFurniture
{
    public class RoomFurnitureDeleteCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;
        private readonly RoomStore _apartmentStore;
        private readonly RoomDetailsHomeVMUI _roomdetailsvmui;

        public RoomFurnitureDeleteCommand(INavigator navigator, IAparmentViewModelFactory factory, RoomStore apartmentStore, RoomDetailsHomeVMUI roomdetailsvmui)
        {
            _navigator = navigator;
            _factory = factory;
            _apartmentStore = apartmentStore;
            _roomdetailsvmui = roomdetailsvmui;
            _roomdetailsvmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _apartmentStore.DeleteFurniture(_roomdetailsvmui.SelectListViewFurniture.IdFur);
            if (result)
            {
                new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
            else
            {
                new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
        private void OnViewModelPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
