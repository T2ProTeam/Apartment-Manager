using AM.UI.State;
using AM.UI.State.Store;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.RoomDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Furniture;
using ViewModel.Room;
using ViewModel.RoomDetails;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadRoomDetailsView : AsyncCommandBase
    {
        private readonly RoomDetailsHomeVMUI _roomdetailhomevm;
        private readonly RoomStore _roomdetails;

        public int _id;

        public LoadRoomDetailsView(RoomDetailsHomeVMUI roomdetailhomevm, RoomStore roomdetails, int id)
        {
            _roomdetailhomevm = roomdetailhomevm;
            _roomdetails = roomdetails;
            _id = id;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _roomdetailhomevm.IsLoading = true;
            try
            {
                List<RoomDetailsImage> roomimage = new List<RoomDetailsImage>();
                List<RoomDetailsFurniture> room = new List<RoomDetailsFurniture>();
                List<FurnitureVm> furniture = new List<FurnitureVm>(); ;
                room = await _roomdetails.LoadRoomDetailsFurniture(_id);
                roomimage = await _roomdetails.LoadRoomDetailsImage(_id);
                // furniture = await _roomdetails.LoadFurniture();
                _roomdetailhomevm.UpdateDataFurniture(room);
                _roomdetailhomevm.UpdateDataImage(roomimage);
                _roomdetailhomevm.UpdateDataComboBoxFurniture(furniture);
            }
            catch (Exception)
            {
                _roomdetailhomevm.MessageError = "Error to Load Room Details Database";
            }
            _roomdetailhomevm.IsLoading = false;
        }
    }
}