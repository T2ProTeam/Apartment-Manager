using AM.UI.Command;
using AM.UI.Command.Bill;
using AM.UI.Command.LoadDataBase;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Bills;
using AM.UI.ViewModelUI.Factory;
using Data.Entity;
using Services.Interface;
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
using ViewModel.Dtos;
using ViewModel.People;

namespace AM.UI.ViewModelUI
{
    public class BillHomeVMUI : ViewModelBase
    {
        private readonly IBill _ibill;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _ViewModelFactory;
        private readonly BillStore _apartmentStore;
        private readonly ComboboxStore _comboboxStore;
        private ObservableCollection<BillVm> _bill;
        public IEnumerable<BillVm> Bill => _bill;

        public ICommand LoadDataBase { get; }

        public ICommand BillUpdateNav { get; }

        public ICommand BillNav { get; }
        public ICommand DeleteConFirm { get; }

        public bool HasData => _bill.Any();

        private string _MessageError;

        public string MessageError
        {
            get { return _MessageError; }
            set
            {
                _MessageError = value;
                OnPropertyChanged(nameof(MessageError));
                OnPropertyChanged(nameof(HasMessageError));
            }
        }

        public bool _Isloading;

        public bool Isloading
        {
            get { return _Isloading; }
            set
            {
                _Isloading = value;
                OnPropertyChanged(nameof(Isloading));
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

        private BillVm _SelectBill;

        public BillVm SelectBill
        {
            get { return _SelectBill; }
            set
            {
                _SelectBill = value;
                OnPropertyChanged(nameof(SelectBill));
            }
        }

        public bool HasMessageError => !string.IsNullOrEmpty(MessageError);

        public BillHomeVMUI(IBill ibill, INavigator navigator, IAparmentViewModelFactory ViewModelFactory, BillStore apartmentStore, ComboboxStore comboboxStore)
        {
            _ibill = ibill;
            _navigator = navigator;
            _ViewModelFactory = ViewModelFactory;
            _apartmentStore = apartmentStore;
            _comboboxStore = comboboxStore;
            BillNav = new UpdateCurrentViewModelCommand(_navigator, _ViewModelFactory);
            _bill = new ObservableCollection<BillVm>();
            LoadDataBase = new LoadBillView(this, apartmentStore);
            LoadDataBase.Execute(null);
            _bill.CollectionChanged += OnReservationsChanged;
            BillUpdateNav = new RelayCommand(NavToBillUpdate);
            DeleteConFirm = new BillDeleteCommand(_navigator, _ViewModelFactory, _apartmentStore, this);
        }

        public void UpdateData(List<BillVm> data)
        {
            foreach (var bill in data)
            {
                _bill.Add(bill);
            }
        }

        public void NavToBillUpdate(object parameter)
        {
            if (parameter is BillVm bill)
            {
                _navigator.CurrentViewModel = new BillUpdateVMUI(_navigator, _ViewModelFactory, _apartmentStore, _comboboxStore, bill);
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}