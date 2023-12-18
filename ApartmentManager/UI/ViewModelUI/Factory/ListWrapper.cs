using AM.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.Factory
{
    public class ListWrapper : ComboboxBase
    {
        public List<RoomForCombobox> RoomForCombobox { get; set; }

        public List<CustomerForCombobox> CustomerForCombobox { get; set; }

        public List<RentalContractForCombobox> RentalContractForCombobox { get; set; }

        public List<FurnitureVm> FurnitureForCombobox { get; set; }
    }
}