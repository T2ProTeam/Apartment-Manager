using AM.UI.State.Store;
using AM.UI.ViewModelUI.Bills;
using AM.UI.ViewModelUI.DepositContract;
using AM.UI.ViewModelUI.RentalContract;
using AM.UI.ViewModelUI.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using ViewModel.Bill;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace AM.UI.Command.LoadDataBase.LoadCombobox
{
    public class LoadAllCombobox : AsyncCommandBase
    {
        private readonly ComboboxStore _comboboxStore;

        private readonly BillAddVMUI _BillAddVM;
        private readonly BillUpdateVMUI _BillUpdateVM;

        private readonly DepositContractAddVMUI _DepositAddVM;
        private readonly DepositContractUpdateVMUI _DepositUpdateVM;

        private readonly RentalContractAddVMUI _RentalAddVM;
        private readonly RentalContractUpdateVMUI _RentalUpdateVM;

        private readonly RoomAddVMUI _RoomAddVM;
        private readonly RoomUpdateVMUI _RoomUpdateVM;

        public int Flag = 0;

        public LoadAllCombobox(ComboboxStore ComboboxStore, BillAddVMUI BillAddVM)
        {
            _comboboxStore = ComboboxStore;
            _BillAddVM = BillAddVM;
            Flag = 1;
        }

        public LoadAllCombobox(ComboboxStore ComboboxStore, BillUpdateVMUI BillUpdateVM)
        {
            _comboboxStore = ComboboxStore;
            _BillUpdateVM = BillUpdateVM;
            Flag = 2;
        }

        public LoadAllCombobox(ComboboxStore ComboboxStore, DepositContractAddVMUI DepositAddVM)
        {
            _comboboxStore = ComboboxStore;
            _DepositAddVM = DepositAddVM;
            Flag = 3;
        }

        public LoadAllCombobox(ComboboxStore ComboboxStore, DepositContractUpdateVMUI DepositUpdateVM)
        {
            _comboboxStore = ComboboxStore;
            _DepositUpdateVM = DepositUpdateVM;
            Flag = 4;
        }

        public LoadAllCombobox(ComboboxStore ComboboxStore, RentalContractAddVMUI RentalAddVM)
        {
            _comboboxStore = ComboboxStore;
            _RentalAddVM = RentalAddVM;
            Flag = 5;
        }

        public LoadAllCombobox(ComboboxStore ComboboxStore, RentalContractUpdateVMUI RentalUpdateVM)
        {
            _comboboxStore = ComboboxStore;
            _RentalUpdateVM = RentalUpdateVM;
            Flag = 6;
        }

        public LoadAllCombobox(ComboboxStore ComboboxStore, RoomAddVMUI RoomAddVM)
        {
            _comboboxStore = ComboboxStore;
            _RoomAddVM = RoomAddVM;
            Flag = 7;
        }

        public LoadAllCombobox(ComboboxStore ComboboxStore, RoomUpdateVMUI RoomUpdateVM)
        {
            _comboboxStore = ComboboxStore;
            _RoomUpdateVM = RoomUpdateVM;
            Flag = 8;
        }

        public override async Task ExecuteAsync(object parameter)

        {
            if (Flag == 1)
            {
                List<RentalContractForCombobox> rental = new List<RentalContractForCombobox>();
                rental = await _comboboxStore.LoadRentalForCombobox();
                _BillAddVM.UpdateRentalForCombobox(rental);
            }
            if (Flag == 2)
            {
                List<RentalContractForCombobox> rental = new List<RentalContractForCombobox>();
                rental = await _comboboxStore.LoadRentalForCombobox();
                _BillUpdateVM.LoadRentalForCombobox(rental);
            }
            if (Flag == 3)
            {
                List<RoomForCombobox> room = new List<RoomForCombobox>();
                List<CustomerForCombobox> customer = new List<CustomerForCombobox>();
                customer = await _comboboxStore.LoadCustomerForCombobox();
                room = await _comboboxStore.LoadRoomForCombobox();
                _DepositAddVM.UpdateDataForRoomCombobox(room);
                _DepositAddVM.UpdateDataForCustomerCombobox(customer);
            }
            if (Flag == 4)
            {
                List<RoomForCombobox> room = new List<RoomForCombobox>();
                List<CustomerForCombobox> customer = new List<CustomerForCombobox>();
                room = await _comboboxStore.LoadRoomForCombobox();
                customer = await _comboboxStore.LoadCustomerForCombobox();
                _DepositUpdateVM.UpdateDataForRoomCombobox(room);
                _DepositUpdateVM.UpdateDataForCustomerCombobox(customer);
            }
            if (Flag == 5)
            {
                List<CustomerForCombobox> customer = new List<CustomerForCombobox>();
                List<RoomForCombobox> room = new List<RoomForCombobox>();
                customer = await _comboboxStore.LoadCustomerForCombobox();
                room = await _comboboxStore.LoadRoomForCombobox();
                _RentalAddVM.UpdateDataForCustomerCombobox(customer);
                _RentalAddVM.UpdateDataForRoomCombobox(room);
            }
            if (Flag == 6)
            {
                List<CustomerForCombobox> customer = new List<CustomerForCombobox>();
                List<RoomForCombobox> room = new List<RoomForCombobox>();
                customer = await _comboboxStore.LoadCustomerForCombobox();
                room = await _comboboxStore.LoadRoomForCombobox();
                _RentalUpdateVM.UpdateDataForCustomerCombobox(customer);
                _RentalUpdateVM.UpdateDataForRoomCombobox(room);
            }
            if (Flag == 7)
            {
                List<CustomerForCombobox> customer = new List<CustomerForCombobox>();
                customer = await _comboboxStore.LoadCustomerForCombobox();
                _RoomAddVM.LoadCustomerCombobox(customer);
            }
        }
    }
}