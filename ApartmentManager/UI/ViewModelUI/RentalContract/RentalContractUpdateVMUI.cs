using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.Command.RentalContract;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Data.Entity;
using Data.Enum;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.RentalContract
{
    public class RentalContractUpdateVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly IRentalContract _irental;
        private readonly RentalContractStore _apartmentStore;
        private readonly ComboboxStore _comboboxStore;
        private RentalContractVm _Rental;

        private ObservableCollection<RentalContractVm> _rentalforColection;
        public IEnumerable<RentalContractVm> rentalforColection => _rentalforColection;

        public ICommand UpdateSuccess { get; }
        public ICommand UpdateConFirm { get; }

        public ICommand LoadDataForCombobox { get; }

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

        public bool HasData => _rentalforColection.Any();

        private ObservableCollection<string> _comboboxActive;

        public IEnumerable<string> comboboxActive => _comboboxActive;

        private Active _Active = Active.Yes;

        public Active Active
        {
            get { return _Active; }
            set
            {
                _Active = value;
                OnPropertyChanged(nameof(Active));
            }
        }

        public ObservableCollection<RoomForCombobox> _comboboxforRoom;
        public IEnumerable<RoomForCombobox> comboboxforRoom => _comboboxforRoom;

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

        public RentalContractUpdateVMUI(INavigator navigator, IAparmentViewModelFactory viewModelFactory, IRentalContract irental, RentalContractStore apartmentStore, RentalContractVm rentalViewModel, ComboboxStore comboboxStore)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _irental = irental;
            _apartmentStore = apartmentStore;
            _Rental = rentalViewModel;
            _comboboxStore = comboboxStore;
            _comboboxActive = new ObservableCollection<string>();
            _comboboxforCustomer = new ObservableCollection<CustomerForCombobox>();
            _comboboxforRoom = new ObservableCollection<RoomForCombobox>();
            UpdateConFirm = new RelayCommand(RentalContractUpdate);
            UpdateSuccess = new UpdateRentalContractCommand(this, _navigator, _viewModelFactory, _apartmentStore);
            LoadDataForCombobox = new LoadAllCombobox(_comboboxStore, this);
            LoadDataForCombobox.Execute(null);

            foreach (Active active in Enum.GetValues(typeof(Active)))
            {
                _comboboxActive.Add(active.ToString());
            }
            _comboboxforRoom.CollectionChanged += OnReservationsChanged;
            _comboboxforCustomer.CollectionChanged += OnReservationsChanged;
        }

        public void UpdateDataForRoomCombobox(List<RoomForCombobox> data)
        {
            data.ForEach(x => _comboboxforRoom.Add(x));
        }

        public void UpdateDataForCustomerCombobox(List<CustomerForCombobox> data)
        {
            data.ForEach(x => _comboboxforCustomer.Add(x));
        }

        public void RentalContractUpdate(object parameter)
        {
            UpdateSuccess.Execute(null);
        }

        public RentalContractVm Rental
        {
            get { return _Rental; }
            set
            {
                Rental = value;
                OnPropertyChanged(nameof(RentalContractVm));
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}