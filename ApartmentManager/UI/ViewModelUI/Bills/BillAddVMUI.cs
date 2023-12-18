using AM.UI.Command.Bill;
using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Data.Enum;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModel.Bill;
using ViewModel.People;
using ViewModel.RentalContract;

namespace AM.UI.ViewModelUI.Bills
{
    public class BillAddVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewmodel;
        private readonly BillStore _billStore;
        private readonly ComboboxStore _comboboxStore;

        private ObservableCollection<BillVm> _BillForCollection;
        public IEnumerable<BillVm> BillForCollection => _BillForCollection;

        public ICommand AddSuccess { get; }
        public ICommand AddConFirm { get; }

        public ICommand LoadComboBoxCommand { get; }

        public bool HasData => _BillForCollection.Any();

        private ObservableCollection<RentalContractForCombobox> _comboboxforRental;
        public IEnumerable<RentalContractForCombobox> comboboxforRental => _comboboxforRental;

        private ObservableCollection<string> _comboboxActive;

        public IEnumerable<string> comboboxActive => _comboboxActive;

        private RentalContractForCombobox _SelectRental;

        public RentalContractForCombobox SelectRental
        {
            get { return _SelectRental; }
            set
            {
                _SelectRental = value;
                OnPropertyChanged(nameof(SelectRental));
            }
        }

        private int _ID;

        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private int _ElectricQuantity;

        public int ElectricQuantity
        {
            get { return _ElectricQuantity; }
            set
            {
                _ElectricQuantity = value;
                OnPropertyChanged(nameof(ElectricQuantity));
            }
        }

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

        private DateTime _PayDate = DateTime.Now;

        public DateTime PayDate
        {
            get { return _PayDate; }
            set
            {
                _PayDate = value;
                OnPropertyChanged(nameof(PayDate));
            }
        }

        private int _TotalMoney;

        public int TotalMoney
        {
            get { return _TotalMoney; }
            set
            {
                _TotalMoney = value;
                OnPropertyChanged(nameof(TotalMoney));
            }
        }

        public BillAddVMUI(INavigator navigator, IAparmentViewModelFactory viewmodel, BillStore billStore, ComboboxStore comboboxStore)
        {
            _comboboxActive = new ObservableCollection<string>();
            _navigator = navigator;
            _viewmodel = viewmodel;
            _billStore = billStore;
            _comboboxStore = comboboxStore;
            foreach (Active active in Enum.GetValues(typeof(Active)))
            {
                _comboboxActive.Add(active.ToString());
            }
            _comboboxforRental = new ObservableCollection<RentalContractForCombobox>();
            LoadComboBoxCommand = new LoadAllCombobox(_comboboxStore, this);
            LoadComboBoxCommand.Execute(null);
            AddSuccess = new BillAddCommand(_navigator, _viewmodel, _billStore, this);
            AddConFirm = new RelayCommand(AddBill);
            _comboboxforRental.CollectionChanged += OnReservationsChanged;
        }

        public void AddBill(object parameter)
        {
            AddSuccess.Execute(null);
        }

        public void UpdateRentalForCombobox(List<RentalContractForCombobox> data)
        {
            data.ForEach(x => _comboboxforRental.Add(x));
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}