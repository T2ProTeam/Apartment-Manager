using AM.UI.ViewModelUI;
using Services.Implement;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Room;
using AM.UI.State;
using AM.UI.State.Store;

namespace AM.UI.Command
{
    public class LoadRoomView : AsyncCommandBase
    {
        private readonly RoomHomeVMUI _roomhomevm;
        private readonly RoomStore _room;

        public LoadRoomView(RoomHomeVMUI roomhomevm, RoomStore room)
        {
            _roomhomevm = roomhomevm;
            _room = room;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _roomhomevm.IsLoading = true;
            try
            {
                await _room.LoadRoom();
                _roomhomevm.UpdateData(_room.roomvm);
            }
            catch (Exception)
            {
                _roomhomevm.MessageError = "Can't load Room Database";
            }
            _roomhomevm.IsLoading = false;
        }
    }
}