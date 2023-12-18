using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.Room;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.People;
using ViewModel.Room;

namespace AM.UI.Command.Room
{
    public class UpdateRoomCommand : AsyncCommandBase
    {
        private readonly IRoom _iroom;
        private readonly RoomUpdateVMUI _roomvmui;
        private readonly RoomStore _apartmentstore;

        public ICommand RoomHomeNav { get; }

        public UpdateRoomCommand(RoomUpdateVMUI roomvmui, INavigator navigator, IAparmentViewModelFactory factory, RoomStore apartmentStore)
        {
            _roomvmui = roomvmui;
            _apartmentstore = apartmentStore;
            RoomHomeNav = new UpdateCurrentViewModelCommand(navigator, factory);
            _roomvmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            RoomUpdateViewModel update = new RoomUpdateViewModel
            {
                ID = _roomvmui.Room.ID,
                Name = _roomvmui.Room.Name,
                Quantity = _roomvmui.Room.Quantity,
            };
            var result = await _apartmentstore.UpdateRoom(update);
            if (result != null)
            {
                new MessageBoxCustom("Update Succeses", MessageType.Success, MessageButtons.Ok).ShowDialog();
                RoomHomeNav.Execute(ViewType.Room);
            }
            else
            {
                new MessageBoxCustom("Update Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }

        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}