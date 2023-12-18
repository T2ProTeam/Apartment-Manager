using AM.UI.ViewModelUI;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.Command
{
    public class CreateRoomView : AsyncCommandBase
    {
        private readonly IRoom _iroom;
        private readonly RoomHomeVMUI _roomhomevm;
        public CreateRoomView(RoomHomeVMUI roomhomevm, IRoom iroom)
        {
            _iroom = iroom;
            _roomhomevm = roomhomevm;
        }

        public override Task ExecuteAsync(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
