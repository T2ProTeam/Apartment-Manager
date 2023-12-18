using AM.UI.Command.DepositContract;
using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.DepositsContract;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.DepositContract
{
    public class DepositContractUpdateVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly IDepositsContract _ifur;
        private readonly DepositContractStore _apartmentStore;
        private readonly ComboboxStore _comboboxStore;
        private DepositsContractVm _depositViewmodel;

        public ICommand UpdateSuccess { get; }
        public ICommand UpdateConFirm { get; }
        public ICommand LoadDataForCombobox { get; }

        public ObservableCollection<RoomForCombobox> _comboboxforRoom;
        public IEnumerable<RoomForCombobox> comboboxforRoom => _comboboxforRoom;

        public ObservableCollection<CustomerForCombobox> _comboboxforCustomer;
        public IEnumerable<CustomerForCombobox> comboboxforCustomer => _comboboxforCustomer;

        private CustomerForCombobox _SelectCustomer;

        public CustomerForCombobox SelectCustomer
        {
            get { return _SelectCustomer; }
            set
            {
                _SelectCustomer = value;
                OnPropertyChanged(nameof(SelectCustomer));
            }
        }

        private RoomForCombobox _SelectRoom;

        public RoomForCombobox SelectRoom
        {
            get { return _SelectRoom; }
            set
            {
                _SelectRoom = value;
                OnPropertyChanged(nameof(SelectRoom));
            }
        }

        public DepositsContractVm DepositViewmodel
        {
            get { return _depositViewmodel; }
            set { _depositViewmodel = value; OnPropertyChanged(nameof(DepositsContractVm)); }
        }

        public DepositContractUpdateVMUI(INavigator navigator, IAparmentViewModelFactory viewModelFactory, IDepositsContract ifur, DepositContractStore apartmentStore, DepositsContractVm depositViewmodel, ComboboxStore comboboxStore)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _ifur = ifur;
            _comboboxforRoom = new ObservableCollection<RoomForCombobox>();
            _apartmentStore = apartmentStore;
            _depositViewmodel = depositViewmodel;
            _comboboxStore = comboboxStore;
            _comboboxforCustomer = new ObservableCollection<CustomerForCombobox>();
            LoadDataForCombobox = new LoadAllCombobox(_comboboxStore, this);
            LoadDataForCombobox.Execute(null);
            UpdateSuccess = new DepositContractUpdateCommand(this, _apartmentStore, _viewModelFactory, _navigator);
            UpdateConFirm = new RelayCommand(UpdateDeposit);
        }

        public void UpdateDeposit(object parameter)
        {
            UpdateSuccess.Execute(null);
        }

        public void UpdateDataForRoomCombobox(List<RoomForCombobox> data)
        {
            data.ForEach(x => _comboboxforRoom.Add(x));
        }

        public void UpdateDataForCustomerCombobox(List<CustomerForCombobox> data)
        {
            data.ForEach(x => _comboboxforCustomer.Add(x));
        }
    }
}