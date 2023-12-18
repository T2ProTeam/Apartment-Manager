using AM.UI.Command;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Customer;
using AM.UI.ViewModelUI.Factory;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModel.Dtos;
using ViewModel.People;

namespace AM.UI.ViewModelUI
{
    public class CustomerVMUI : ViewModelBase
    {
        private ObservableCollection<CustomerVM> _test;
        private readonly IPeople _people;
        private readonly IAparmentViewModelFactory _factory;
        private readonly ApartmentStore _apartmentStore;
        private readonly ComboboxStore _combobox;

        private CustomerVM _customer;

        public CustomerVM Customer
        {
            get { return _customer; }
            set { _customer = value; OnPropertyChanged(nameof(Customer)); }
        }

        private int _ID;

        public int ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private string _search;

        public bool HasData => _test.Any();
        private bool _isText;

        public bool IsText
        {
            get
            {
                return _isText;
            }
            set
            {
                _isText = value;
                OnPropertyChanged(nameof(IsText));
            }
        }

        public string search
        {
            get { return _search; }
            set
            {
                _search = value;// OnPropertyChanged(nameof(search));
                ChangedString(nameof(search));
            }
        }

        public IEnumerable<CustomerVM> test => _test;

        private string _errorMessage;

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));

                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
        private bool _isLoading;

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private readonly INavigator _navigator;
        public ICommand UpdateNavCustomer { get; }
        public ICommand AddNavCustomer { get; }

        public ICommand LoadDatabase { get; }
        public ICommand DeleteCustomerCommand { get; }
        public ICommand ConfirmDeleteCustomerCommand { get; }

        public CustomerVMUI(INavigator navigator, IPeople people, IAparmentViewModelFactory aparmentViewModelFactory, ApartmentStore apartmentStore, ComboboxStore comboboxStore)
        {
            UpdateNavCustomer = new RelayCommand(NavigateUpdateCustomerVM);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer);
            ConfirmDeleteCustomerCommand = new DeleteCustomerCommand(this, apartmentStore, aparmentViewModelFactory, navigator);
            AddNavCustomer = new UpdateCurrentViewModelCommand(navigator, aparmentViewModelFactory);
            LoadDatabase = new LoadCustomerView(this, apartmentStore);

            _test = new ObservableCollection<CustomerVM>();
            _isText = true;
            LoadDatabase.Execute(null);
            _people = people;
            _apartmentStore = apartmentStore;
            _navigator =navigator;
            _factory = aparmentViewModelFactory;
            _combobox = comboboxStore;
            _test.CollectionChanged += OnReservationsChanged;
            _apartmentStore.YouTubeViewerDeleted += Store_Delete;
            _apartmentStore.CustomerAdd+=  Store_Add;
            _apartmentStore.CreatePeopleInRental+= Store_Add_Rental;
            _apartmentStore.CustomerUpdate += Store_Update;
        }

        public async void DeleteCustomer(object parameter)
        {
            if (parameter is CustomerVM person)
            {
                bool? Confirm = new MessageBoxCustom($"Do you want to delete customer :{person.ID} ", MessageType.Confirmation, MessageButtons.YesNo).ShowDialog();
                if (Confirm == true)
                {
                    _ID = person.ID;
                    ConfirmDeleteCustomerCommand.Execute(null);
                }
            }
        }

        private void Store_Add(CustomerVM Data)
        {
            _test.Add(Data);
        }

        private void Store_Add_Rental(List<CustomerVM> Data)
        {
            Data.ForEach(x =>
            {
                _test.Add(x);
            });
        }

        private void Store_Delete(int id)
        {
            var object1 = _test.FirstOrDefault(y => y.ID == id);

            if (object1 != null)
            {
                _test.Remove(object1);
            }
        }

        private void Store_Update(CustomerVM Data)
        {
        }

        private void NavigateUpdateCustomerVM(object parameter)
        {
            _navigator.CurrentViewModel = new UpdateCustomerVMUI(_apartmentStore, Customer, _navigator, _factory);
        }

        private void ChangedString(string _search)
        {
            IsText =false;

            if (search.Equals(""))
            {
                if (test.Any())
                    _test.Clear();
                LoadDatabase.Execute(null);
                IsText = true;
            }
            else
            {
                if (int.TryParse(search, out int intValue))
                {
                    IsLoading = true;
                    if (_test.Any())
                        _test.Clear();
                    LoadDatabase.Execute(null);
                    ObservableCollection<CustomerVM> find = new ObservableCollection<CustomerVM>();
                    foreach (CustomerVM item in _test)
                        if (item.ID==intValue)
                            find.Add(item);
                    if (_test.Any())
                        _test.Clear();
                    foreach (CustomerVM item in find)
                    {
                        _test.Add(item);
                    }

                    IsLoading = false;
                }
                else if (test.Any()) _test.Clear();
            }
            OnPropertyChanged(nameof(search));
        }

        public void UpdateData(List<CustomerVM> data)
        {
            _test.Clear();
            foreach (var item in data)
            {
                _test.Add(item);
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}