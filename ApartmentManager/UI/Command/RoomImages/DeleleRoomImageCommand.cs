using AM.UI.State;
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
using System.Windows;

namespace AM.UI.Command.RoomImages
{
    public class DeleleRoomImageCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;
        private readonly RoomStore _apartmentStore;
        private readonly RoomDetailsHomeVMUI _roomdetailsvm;

        public DeleleRoomImageCommand(INavigator navigator, IAparmentViewModelFactory factory, RoomStore apartmentStore, RoomDetailsHomeVMUI roomdetailsvm)
        {
            _navigator = navigator;
            _factory = factory;
            _apartmentStore = apartmentStore;
            _roomdetailsvm = roomdetailsvm;
            _roomdetailsvm.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _apartmentStore.DeteleImage(_roomdetailsvm.RoomImages.IDImage);
            if (result == 1)
            {
                new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();

               // _navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.RoomDetails);
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
