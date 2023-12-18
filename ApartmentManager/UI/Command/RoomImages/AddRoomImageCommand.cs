using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.RoomDetails;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.RoomImage;

namespace AM.UI.Command.RoomImages
{
    public class AddRoomImageCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;
        private readonly RoomStore _apartmentStore;
        private readonly RoomDetailsAddImageVMUI _roomdetailsvmui;

        public AddRoomImageCommand(RoomDetailsAddImageVMUI roomdetailsvmui, INavigator navigator, IAparmentViewModelFactory factory, RoomStore apartmentStore)
        {
            _roomdetailsvmui = roomdetailsvmui;
            _navigator = navigator;
            _factory = factory;
            _apartmentStore = apartmentStore;
            _roomdetailsvmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            int count = 0;
            foreach (RoomImageCreateViewModel roomimage in _roomdetailsvmui.images)
            {
                var result = await _apartmentStore.CreateImage(roomimage, roomimage.FileName);
                if (result)
                {
                    count++;
                }
                else
                {
                    new MessageBoxCustom("Fail", MessageType.Success, MessageButtons.Ok).ShowDialog();
                }
            }
            if (count == _roomdetailsvmui.images.Count)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.RoomDetails);
            }
        }

        private void OnViewModelPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}