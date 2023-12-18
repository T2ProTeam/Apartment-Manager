using AM.UI.Command;
using AM.UI.Command.LoadDataBase;
using AM.UI.Command.RentalContract;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.RentalContract;
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
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace AM.UI.ViewModelUI
{
    public class RentalContractHomeVMUI : ViewModelBase
    {
        private ObservableCollection<RentalContractVm> _rental;
        private readonly INavigator _navigator;
        private readonly IRentalContract _irental;
        private readonly IAparmentViewModelFactory _ViewModel;
        private readonly RentalContractStore _apartmentStore;
        private readonly RoomStore _roomStore;
        private readonly ComboboxStore _comboboxStore;
        public IEnumerable<RentalContractVm> Rental => _rental;

        public ICommand LoadDataBase { get; }

        public ICommand RentalNav { get; }

        public ICommand RentalUpdateNavCommand { get; }

        public ICommand RentalDeleteConFirm { get; }

        public ICommand RentalDeleteSuccess { get; }

        private bool _IsLoading;

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

        public bool HasData => _rental.Any();

        private string _ErrorMessage;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set
            {
                _ErrorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasMessageError));
            }
        }

        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged(nameof(ID)); }
        }

        private RentalContractVm _SelectRentalContract;

        public RentalContractVm SelectRentalContract
        {
            get { return _SelectRentalContract; }
            set
            {
                _SelectRentalContract = value;
                OnPropertyChanged(nameof(SelectRentalContract));
            }
        }

        public bool HasMessageError => !string.IsNullOrEmpty(ErrorMessage);

        public RentalContractHomeVMUI(INavigator navigator, IAparmentViewModelFactory ViewModel, RentalContractStore apartmentStore, IRentalContract irental, RoomStore roomStore, ComboboxStore comboboxStore)
        {
            _navigator = navigator;
            _ViewModel = ViewModel;
            _apartmentStore = apartmentStore;
            _rental = new ObservableCollection<RentalContractVm>();
            LoadDataBase = new LoadRentalContractView(this, apartmentStore);
            LoadDataBase.Execute(null);
            _irental = irental;
            RentalUpdateNavCommand = new RelayCommand(RentalUpdateNav);
            RentalNav = new UpdateCurrentViewModelCommand(_navigator, _ViewModel);
            RentalDeleteConFirm = new DeleteRentalContractCommand(this, _apartmentStore, _navigator, _ViewModel);
            _roomStore = roomStore;
            _roomStore.LoadAgainForRentalContract += UpdateRentalWhenRoomUpdate;
            _rental.CollectionChanged += OnReservationsChanged;
            _apartmentStore.RentalContractDelete += Delete_Store;
            _comboboxStore = comboboxStore;
        }

        public void RentalUpdateNav(object parameter)
        {
            if (parameter is RentalContractVm rental)
            {
                _navigator.CurrentViewModel = new RentalContractUpdateVMUI(_navigator, _ViewModel, _irental, _apartmentStore, rental, _comboboxStore);
            }
        }

        public void LoadDataBaseRental()
        {
            LoadDataBase.Execute(null);
        }

        public async void Delete_Store(int id)
        {
            var object1 = _rental.FirstOrDefault(x => x.ID == id);
            if (object1 != null)
                _rental.Remove(object1);
        }

        public void UpdateRentalWhenRoomUpdate(RoomVm data)
        {
            RentalContractVm rental = new RentalContractVm
            {
                RoomName = data.Name
            };
            var current = _rental.ToList().FindIndex(x => x.RoomName == data.Name);
            if (current != -1)
            {
                _rental[current].RoomName = rental.RoomName;
            }
        }

        public void UpdateData(List<RentalContractVm> data)
        {
            foreach (var rental in data)
            {
                _rental.Add(rental);
            }
        }

        private void ChangedString(string _Search)
        {
            IsText = false;

            if (search.Equals(""))
            {
                if (_rental.Any())
                    _rental.Clear();
                LoadDataBase.Execute(null);
                IsText = true;
            }
            else
            {
                if (int.TryParse(search, out int intValue))
                {
                    IsLoading = true;
                    if (_rental.Any())
                        _rental.Clear();
                    LoadDataBase.Execute(null);
                    ObservableCollection<RentalContractVm> find = new ObservableCollection<RentalContractVm>();
                    foreach (RentalContractVm item in _rental)
                        if (item.ID == intValue)
                            find.Add(item);
                    if (_rental.Any())
                        _rental.Clear();
                    foreach (RentalContractVm item in find)
                    {
                        _rental.Add(item);
                    }

                    IsLoading = false;
                }
                else if (_rental.Any()) _rental.Clear();
            }
            OnPropertyChanged(nameof(search));
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}