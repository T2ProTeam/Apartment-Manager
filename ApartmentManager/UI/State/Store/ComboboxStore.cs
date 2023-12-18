using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace AM.UI.State.Store
{
    public class ComboboxStore
    {
        private readonly IFurniture _ifur;
        private readonly IPeople _ipeople;
        private readonly IRoom _iroom;
        private readonly IRentalContract _irental;

        public ComboboxStore(IFurniture ifur, IPeople ipeople,IRoom iroom, IRentalContract irental)
        {
            _ifur = ifur;
            _ipeople = ipeople;
            _iroom = iroom;
            _irental = irental;
        }

        public async Task<List<FurnitureVm>> LoadFurniture()
        {
            return await _ifur.GetAll();
        }


        public async Task<List<CustomerForCombobox>> LoadCustomerForCombobox()
        {
            return await _ipeople.GetIdNameForCombobox();
        }

        public async Task<List<RoomForCombobox>> LoadRoomForCombobox()
        {
            return await _iroom.GetIdNameRoom();
        }

        public async Task<List<RentalContractForCombobox>> LoadRentalForCombobox()
        {
            return await _irental.GetAllRental();
        }
    }
}
