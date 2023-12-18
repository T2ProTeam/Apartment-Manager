using AM.UI.Command;
using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.Command.Room;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Caliburn.Micro;
using Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModel.People;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.Room
{
    public class RoomAddVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly IRoom _iroom;
        private readonly RoomStore _apartmentStore;
        private readonly ComboboxStore _comboboxStore;

        private readonly RoomCreateViewModel _room;
        private ObservableCollection<CustomerForCombobox> _roomHasData;
        public IEnumerable<CustomerForCombobox> roomHasData => _roomHasData;

        public bool HasData => _roomHasData.Any();

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

        public ICommand RoomCreateSuccess { get; }
        public ICommand RoomCreateConFirm { get; }
        public ICommand LoadCustomerData { get; }
        public ICommand RoomHomeNav { get; }

        private int _iD;

        public int iD
        {
            get { return _iD; }
            set
            {
                _iD = value;
                OnPropertyChanged(nameof(iD));
            }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private int _quantity;

        public int quantity
        {
            get { return _quantity; }
            set
            {
                OnPropertyChanged(nameof(quantity));
                _quantity = value;
            }
        }

        private bool _isvalid = true;

        public bool IsValid
        {
            get
            {
                return _isvalid;
            }
            set
            {
                _isvalid = value;
                OnPropertyChanged(nameof(IsValid));
            }
        }

        private void validation()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            IsValid= Validator.TryValidateObject(this, context, results, true);
            OnPropertyChanged(nameof(IsValid));
        }

        public RoomAddVMUI(IRoom iroom, INavigator navigator, IAparmentViewModelFactory viewModelFactory, RoomStore apartmentStore, ComboboxStore ComboboxStore)
        {
            _iroom = iroom;
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _apartmentStore = apartmentStore;
            _comboboxStore = ComboboxStore;
            LoadCustomerData = new LoadAllCombobox(_comboboxStore, this);
            LoadCustomerData.Execute(null);
            RoomCreateSuccess = new AddRoomCommand(this, apartmentStore, navigator, viewModelFactory);
            _comboboxforCustomer = new ObservableCollection<CustomerForCombobox>();
            RoomCreateConFirm = new RelayCommand(CreateRoom);
            _comboboxforCustomer.CollectionChanged += OnReservationsChanged;
            RoomHomeNav = new UpdateCurrentViewModelCommand(_navigator, _viewModelFactory);
        }

        public void CreateRoom(object parameter)
        {
            //RoomCreateSuccess.Execute(null);
        }

        public void LoadCustomerCombobox(List<CustomerForCombobox> data)
        {
            data.ForEach(x => _comboboxforCustomer.Add(x));
        }

        public RoomCreateViewModel Room
        {
            get => _room;
            set
            {
                Room = value;
                OnPropertyChanged();
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}