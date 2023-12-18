using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.Room;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AM.UI.Command.Room
{
    public class RoomDeleteCommand : AsyncCommandBase
    {
        private readonly RoomHomeVMUI _roomvmui;
        private readonly RoomStore _apartmentStore;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;

        public RoomDeleteCommand(RoomHomeVMUI roomvmui, RoomStore apartmentStore, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _roomvmui = roomvmui;
            _apartmentStore = apartmentStore;
            _navigator = navigator;
            _factory = factory;
            _roomvmui.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var result = await _apartmentStore.DeleteRoom(_roomvmui.SelectRoom.ID);
            if(result == true)
            {
                new MessageBoxCustom("Delete Successed", MessageType.Success, MessageButtons.Ok).ShowDialog();
                _navigator.CurrentViewModel = _factory.CreateViewModel(ViewType.Room);
            }
            else
            {
                new MessageBoxCustom("Delete Fail", MessageType.Warning, MessageButtons.Ok).ShowDialog();
            }
        }
        public void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecutedChanged();
        }
    }
}
