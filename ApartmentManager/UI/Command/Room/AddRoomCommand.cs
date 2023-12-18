using AM.UI.State.Navigators;
using AM.UI.State;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using ViewModel.Room;
using AM.UI.ViewModelUI.Room;
using AM.UI.View.Dialog;
using System.Windows;
using AM.UI.State.Store;

namespace AM.UI.Command.Room
{
    public class AddRoomCommand : AsyncCommandBase
    {
        private readonly RoomAddVMUI _roomvmui;
        private readonly RoomStore _room;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;

        public AddRoomCommand(RoomAddVMUI roomvmui, RoomStore room, INavigator navigator, IAparmentViewModelFactory viewModelFactory)
        {
            _roomvmui = roomvmui;
            _room = room;
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _roomvmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            RoomCreateViewModel create = new RoomCreateViewModel
            {
                Name = _roomvmui.name,
                Quantity = _roomvmui.quantity
            };
            var result = await _room.AddRoom(create);
            if (result != null)
            {
                new MessageBoxCustom("Add Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Room);
            }
            else
            {
                new MessageBoxCustom("Fail", MessageType.Success, MessageButtons.Ok).ShowDialog();
            }
        }

        private void OnViewModelPropertyChanged(Object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}