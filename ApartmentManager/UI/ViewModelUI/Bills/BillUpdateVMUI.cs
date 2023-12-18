using AM.UI.Command.Bill;
using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Data.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Bill;
using ViewModel.RentalContract;

namespace AM.UI.ViewModelUI.Bills
{
    public class BillUpdateVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewmodel;
        private readonly BillStore _billStore;
        private readonly ComboboxStore _comboboxStore;
        private ObservableCollection<BillVm> _BillForCollection;
        public IEnumerable<BillVm> BillForCollection => _BillForCollection;

        public ICommand UpdateSuccess { get; }
        public ICommand UpdateConFirm { get; }

        public ICommand LoadComboboxCommand { get; }

        public bool HasData => _BillForCollection.Any();
        public ObservableCollection<RentalContractForCombobox> _comboboxforRental;
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

        private BillVm _BillVm;

        public BillVm BillVm
        {
            get { return _BillVm; }
            set
            {
                BillVm = value;
                OnPropertyChanged(nameof(BillVm));
            }
        }

        public BillUpdateVMUI(INavigator navigator, IAparmentViewModelFactory viewmodel, BillStore billStore, ComboboxStore comboboxStore, BillVm billVm)
        {
            _comboboxActive = new ObservableCollection<string>();
            _navigator = navigator;
            _viewmodel = viewmodel;
            _billStore = billStore;
            foreach (Active active in Enum.GetValues(typeof(Active)))
            {
                _comboboxActive.Add(active.ToString());
            }
            _comboboxforRental = new ObservableCollection<RentalContractForCombobox>();
            _comboboxStore = comboboxStore;
            _BillVm = billVm;
            LoadComboboxCommand = new LoadAllCombobox(_comboboxStore, this);
            LoadComboboxCommand.Execute(null);
            UpdateSuccess = new BillUpdateCommand(_navigator, _viewmodel, _billStore, this);
            UpdateConFirm = new RelayCommand(UpdateBill);
            _comboboxforRental.CollectionChanged += OnReservationsChanged;
        }

        public void UpdateBill(object parameter)
        {
            UpdateSuccess.Execute(null);
        }

        public void LoadRentalForCombobox(List<RentalContractForCombobox> data)
        {
            data.ForEach(x => _comboboxforRental.Add(x));
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}