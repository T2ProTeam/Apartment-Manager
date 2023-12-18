using AM.UI.Command;
using AM.UI.Command.DepositContract;
using AM.UI.Command.LoadDataBase;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Microsoft.Identity.Client;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModel.DepositsContract;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.DepositContract
{
    public class DepositContractHomeVMUI : ViewModelBase
    {
        private readonly IDepositsContract _ideposit;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _ViewModelFactory;
        private readonly DepositContractStore _apartmentStore;
        private readonly ComboboxStore _comboboxStore;
        private readonly RoomStore _roomStore;
        private ObservableCollection<DepositsContractVm> _deposit;

        public ICommand LoadDataBase { get; }

        public ICommand DepositContractUpdateNav { get; }

        public ICommand DepositNav { get; }

        public ICommand DeleteConFirm { get; }

        public IEnumerable<DepositsContractVm> Deposit => _deposit;

        public bool HasData => _deposit.Any();

        public bool _IsLoading;

        public bool IsLoading
        {
            get { return _IsLoading; }
            set
            {
                _IsLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _search;

        public string search
        {
            get { return _search; }
            set
            {
                _search = value;
                OnPropertyChanged(nameof(ChangedString));
            }
        }

        private bool _isText;

        public bool IsText
        {
            get { return _isText; }
            set
            {
                _isText = value;
                OnPropertyChanged(nameof(IsText));
            }
        }

        public string _MessageError;

        public string MessageError
        {
            get { return _MessageError; }
            set
            {
                _MessageError = value;
                OnPropertyChanged(nameof(MessageError));
            }
        }

        private DepositsContractVm _SelectDeposits;

        public DepositsContractVm SelectDeposits
        {
            get { return _SelectDeposits; }
            set
            {
                _SelectDeposits = value;
                OnPropertyChanged(nameof(SelectDeposits));
            }
        }

        public bool HasMessageError => !string.IsNullOrEmpty(MessageError);

        public DepositContractHomeVMUI(IDepositsContract ideposit, INavigator navigator, IAparmentViewModelFactory ViewModelFactory, DepositContractStore apartmentStore, ComboboxStore comboboxStore, RoomStore roomStore)
        {
            _ideposit = ideposit;
            _navigator = navigator;
            _ViewModelFactory = ViewModelFactory;
            _apartmentStore = apartmentStore;
            DepositNav = new RelayCommand(NavToadd);
            _deposit = new ObservableCollection<DepositsContractVm>();
            LoadDataBase = new LoadDepositContractview(this, apartmentStore);
            LoadDataBase.Execute(null);
            _deposit.CollectionChanged += OnReservationsChanged;
            _comboboxStore = comboboxStore;
            DepositContractUpdateNav = new RelayCommand(NavToDepositUpdate);
            DeleteConFirm = new DepositContractDeleteCommand(this, _apartmentStore, _navigator, _ViewModelFactory);
            _roomStore = roomStore;
            _roomStore.LoadAgainForDepositContract += UpdateDepositWhenRoomUpdate;
        }

        public void NavToadd(object parameter)
        {
            _navigator.CurrentViewModel = new DepositContractAddVMUI(_apartmentStore, _navigator, _ViewModelFactory, _comboboxStore);
        }

        public void UpdateData(List<DepositsContractVm> data)
        {
            data.ForEach(x => _deposit.Add(x));
        }

        public async void UpdateDepositWhenRoomUpdate(RoomVm data)
        {
            DepositsContractVm deposit = new DepositsContractVm()
            {
                RoomName = data.Name
            };
            var current = _deposit.ToList().FindIndex(x => x.ID == data.ID);
            if (current != -1)
            {
                _deposit[current].RoomName = deposit.RoomName;
            }
        }

        public void NavToDepositUpdate(object parameter)
        {
            if (parameter is DepositsContractVm deposit)
            {
                _navigator.CurrentViewModel = new DepositContractUpdateVMUI(_navigator, _ViewModelFactory, _ideposit, _apartmentStore, deposit, _comboboxStore);
            }
        }

        private void ChangedString(string _Search)
        {
            IsText = false;

            if (search.Equals(""))
            {
                if (_deposit.Any())
                    _deposit.Clear();
                LoadDataBase.Execute(null);
                IsText = true;
            }
            else
            {
                if (int.TryParse(search, out int intValue))
                {
                    IsLoading = true;
                    if (_deposit.Any())
                        _deposit.Clear();
                    LoadDataBase.Execute(null);
                    ObservableCollection<DepositsContractVm> find = new ObservableCollection<DepositsContractVm>();
                    foreach (DepositsContractVm item in _deposit)
                        if (item.ID == intValue)
                            find.Add(item);
                    if (_deposit.Any())
                        _deposit.Clear();
                    foreach (DepositsContractVm item in find)
                    {
                        _deposit.Add(item);
                    }

                    IsLoading = false;
                }
                else if (_deposit.Any()) _deposit.Clear();
            }
            OnPropertyChanged(nameof(search));
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}