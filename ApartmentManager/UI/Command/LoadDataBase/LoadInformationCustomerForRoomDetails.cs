using AM.UI.State.Store;
using AM.UI.ViewModelUI.RoomDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;

namespace AM.UI.Command.LoadDataBase
{
    public class LoadInformationCustomerForRoomDetails : AsyncCommandBase
    {
        private readonly RoomStore _store;
        private readonly RoomDetailsInformationCustomerVMUI _customer;
        public int _IDRoom;
        public LoadInformationCustomerForRoomDetails(RoomStore store, RoomDetailsInformationCustomerVMUI customer,int IDRoom)
        {
            _store = store;
            _customer = customer;
            _IDRoom = IDRoom;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            List<CustomerVM> cus = new List<CustomerVM>();
            cus = await _store.GetAllCustomerInRoomByIDRoom(_IDRoom);
            _customer.TakeNameForListView(cus);
        }
    }
}
