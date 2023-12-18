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
using ViewModel.Furniture;
using ViewModel.RoomDetails;

namespace AM.UI.Command.RoomFurniture
{
    public class RoomFurnitureAddCommand : AsyncCommandBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;
        private readonly RoomStore _apartmentStore;
        private readonly RoomDetailsHomeVMUI _roomdetailsvmui;

        public RoomFurnitureAddCommand(RoomDetailsHomeVMUI roomdetailsvmui, INavigator navigator, IAparmentViewModelFactory factory, RoomStore apartmentStore)
        {
            _roomdetailsvmui = roomdetailsvmui;
            _navigator = navigator;
            _factory = factory;
            _apartmentStore = apartmentStore;
            _roomdetailsvmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            RoomDetailsVm create = new RoomDetailsVm
            {
                IDFur = _roomdetailsvmui.SelectedFurniture.ID,
                IDRoom = _roomdetailsvmui.LoadInformationRoom.ID
            };
            var result = await _apartmentStore.CreateFurniture(create);
            if (result != null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                //_navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.RoomDetails);
            }
            else
                new MessageBoxCustom("Add Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
        }

        private void OnViewModelPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
