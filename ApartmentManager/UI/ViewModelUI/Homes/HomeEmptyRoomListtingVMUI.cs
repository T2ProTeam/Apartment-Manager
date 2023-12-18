using AM.UI.Command;
using AM.UI.Command.Home;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel.Home;
using ViewModel.Room;

namespace AM.UI.ViewModelUI
{
    public class HomeEmptyRoomListtingVMUI : ViewModelBase
    {
        private readonly HomeStore _HomeStore;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;
        private ObservableCollection<RoomVm> _listemptyroom;

        public IEnumerable<RoomVm> ListEmptyRoom => _listemptyroom;
        public bool HasData => _listemptyroom.Any();

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

        public ICommand ReturnBillHomeVM { get; }
        public ICommand NavtoDepositAdd { get; }
        public ICommand LoadDataCommand { get; }

        public HomeEmptyRoomListtingVMUI(IHome home, HomeStore homeStore, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _HomeStore = homeStore;
            _navigator = navigator;
            _factory = factory;
            _listemptyroom = new ObservableCollection<RoomVm>();
            ReturnBillHomeVM = new UpdateCurrentHomeViewModelCommand(home, homeStore, navigator, factory);
            NavtoDepositAdd = new UpdateCurrentViewModelCommand(_navigator, _factory);
            LoadDataCommand = new LoadEmptyRoomListtingCommand(this, homeStore);
            LoadDataCommand.Execute(null);
            _listemptyroom.CollectionChanged += OnReservationsChanged;
        }

        public void UpdateData(List<RoomVm> rooms)
        {
            _listemptyroom.Clear();
            rooms.ForEach(x => _listemptyroom.Add(x));
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}