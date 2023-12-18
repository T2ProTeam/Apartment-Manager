using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.Command.RentalContract;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Data.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.RentalContract
{
    public class RentalContractAddVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly RentalContractStore _apartmentStore;
        private readonly RentalContractCreateViewModel _rental;
        private readonly ComboboxStore _comboboxStore;
        private ObservableCollection<RentalContractVm> _rentalforColection;
        public IEnumerable<RentalContractVm> rentalforColection => _rentalforColection;

        public ICommand CreateSuccess { get; }

        public ICommand CreateConFirm { get; }

        public ICommand LoadDataForCombobox { get; }

        //Properties

        private DateTime _ReceiveDate = DateTime.Now;

        public DateTime ReceiveDate
        {
            get { return _ReceiveDate; }
            set { _ReceiveDate = value; OnPropertyChanged(nameof(ReceiveDate)); }
        }

        private DateTime _CheckOutDate = DateTime.Now;

        public DateTime CheckOutDate
        {
            get { return _CheckOutDate; }
            set
            {
                _CheckOutDate = value;

                OnPropertyChanged(nameof(CheckOutDate));
            }
        }

        private int _RoomMoney;

        [Range(0, int.MaxValue, ErrorMessage = "RoomMoney must be a positive number.")]
        public int RoomMoney
        {
            get { return _RoomMoney; }
            set
            { _RoomMoney = value; OnPropertyChanged(nameof(RoomMoney)); }
        }

        private int _ELectricMoney;

        [Range(0, int.MaxValue, ErrorMessage = "RoomMoney must be a positive number.")]
        public int ELectricMoney
        {
            get { return _ELectricMoney; }
            set
            { _ELectricMoney = value; OnPropertyChanged(nameof(ELectricMoney)); }
        }

        private int _WaterMoney;

        [Range(0, int.MaxValue, ErrorMessage = "RoomMoney must be a positive number.")]
        public int WaterMoney
        {
            get { return _WaterMoney; }
            set
            { _WaterMoney = value; OnPropertyChanged(nameof(WaterMoney)); }
        }

        private int _ServiceMoney;

        [Range(0, int.MaxValue, ErrorMessage = "RoomMoney must be a positive number.")]
        public int ServiceMoney
        {
            get { return _ServiceMoney; }
            set
            { _ServiceMoney = value; OnPropertyChanged(nameof(ServiceMoney)); }
        }

        public bool IsValid
        {
            get
            {
                var context = new ValidationContext(this);
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

                bool isValid = Validator.TryValidateObject(this, context, results, true);

                return isValid;
            }
        }

        public bool HasData => _rentalforColection.Any();

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

        public RentalContractAddVMUI(INavigator navigator, IAparmentViewModelFactory viewModelFactory, RentalContractStore apartmentStore, ComboboxStore comboboxStore)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _apartmentStore = apartmentStore;
            _comboboxStore = comboboxStore;
            _comboboxActive = new ObservableCollection<string>();
            foreach (Active active in Enum.GetValues(typeof(Active)))
            {
                _comboboxActive.Add(active.ToString());
            }
            _comboboxforCustomer = new ObservableCollection<CustomerForCombobox>();
            _comboboxforRoom = new ObservableCollection<RoomForCombobox>();
            CreateConFirm = new RelayCommand(AddRentalContract);
            CreateSuccess = new AddRentalContractCommand(this, _apartmentStore, _navigator, _viewModelFactory);
            LoadDataForCombobox = new LoadAllCombobox(_comboboxStore, this);
            LoadDataForCombobox.Execute(null);
            _comboboxforRoom.CollectionChanged += OnReservationsChanged;
            _comboboxforCustomer.CollectionChanged += OnReservationsChanged;
        }

        public void AddRentalContract(object parameter)
        {
            CreateSuccess.Execute(null);
        }

        public void UpdateDataForCustomerCombobox(List<CustomerForCombobox> data)
        {
            data.ForEach(x => _comboboxforCustomer.Add(x));
        }

        public void UpdateDataForRoomCombobox(List<RoomForCombobox> data)
        {
            data.ForEach(x => _comboboxforRoom.Add(x));
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}