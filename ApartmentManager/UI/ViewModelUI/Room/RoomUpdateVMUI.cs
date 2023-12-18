using AM.UI.Command;
using AM.UI.Command.LoadDataBase;
using AM.UI.Command.LoadDataBase.LoadCombobox;
using AM.UI.Command.Room;
using AM.UI.State;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Caliburn.Micro;
using Data.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;
using ViewModel.RoomDetails;

namespace AM.UI.ViewModelUI.Room
{
    public class RoomUpdateVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewModelFactory;
        private readonly IRoom _iroom;
        private readonly RoomStore _apartmentStore;

        private readonly ComboboxStore _comboboxStore;
        private readonly RentalContractHomeVMUI rental;
        private ObservableCollection<CustomerVM> _room;
        public IEnumerable<CustomerVM> Roomm => _room;

        private readonly RoomUpdateViewModel _roomUpdateViewModel;

        public RoomUpdateViewModel Room
        {
            get => _roomUpdateViewModel;
            set
            {
                Room = value;
                OnPropertyChanged(nameof(Room));
            }
        }

        public string Name
        {
            get => _roomUpdateViewModel.Name;
            set
            {
                _roomUpdateViewModel.Name = value;
                Onchanged(nameof(Name));
            }
        }

        public int Quantity
        {
            get => _roomUpdateViewModel.Quantity;
            set
            {
                _roomUpdateViewModel.Quantity = value;
                Onchanged(nameof(Quantity));
            }
        }

        public ICommand LoadCustomerData { get; }
        public ICommand UpdateConfirm { get; }

        public ICommand RoomHomeNav { get; }

        public ICommand UpdateSuccess { get; }

        public ICommand Checkcombo { get; }

        public bool HasData => _room.Any();

        public RoomUpdateVMUI(IRoom iroom, RoomVm model, INavigator navigator, IAparmentViewModelFactory viewModelFactory, RoomStore apartmentStore, ComboboxStore ComboboxStore)
        {
            _viewModelFactory = viewModelFactory;
            _iroom = iroom;
            _roomUpdateViewModel = new RoomUpdateViewModel
            {
                ID = model.ID,
                Name = model.Name,
                Quantity = model.Quantity
            };
            _comboboxStore = ComboboxStore;
            _navigator = navigator;
            _apartmentStore = apartmentStore;
            LoadCustomerData = new LoadAllCombobox(_comboboxStore, this);
            LoadCustomerData.Execute(null);
            UpdateSuccess = new UpdateRoomCommand(this, navigator, viewModelFactory, apartmentStore);
            UpdateConfirm = new RelayCommand(UpdateRoom);
            RoomHomeNav = new UpdateCurrentViewModelCommand(_navigator, _viewModelFactory);
        }

        private void Onchanged(string name)
        {
            OnPropertyChanged(nameof(name));
            OnPropertyChanged(nameof(Room));
        }

        public void UpdateRoom(object parameter)
        {
            UpdateSuccess.Execute(null);
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}