using AM.UI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.State.Navigators
{
    public enum ViewType
    {
        Home,
        Customer, CustomerAdd,
        Room, RoomAdd, RoomUpdate,
        RoomDetails,
        Furnitures, FurnituresAdd, FurnitureUpdate,
        RentalContract, RentalContractAdd, RentalContractUpdate,
        PaymentExtensionHome, PaymentExtensionAdd, PaymentExtensionUpdate,
        Bill, BillAdd, BillUpdate,
        DepositContract, DepositContractAdd, DepositContractUpdate,
        RoomImagesAdd, RoomDetailsInformationCustomer,
        Statistics
    }

    /* public enum ComboBoxType
     {
         BillAdd, BillUpdate,
         DepositAdd, DepositUpdate,
         RentalAdd, RentalUpdate,
         RoomAdd, RoomUpdate
     }*/

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ViewModelBase CurrentHomeViewModel { get; set; }

        //ComboboxBase LoadCurrentComboBox { get; set; }

        event Action StateChanged;

        //object LoadCurrentComboBox(ComboBoxType roomAdd);
    }
}