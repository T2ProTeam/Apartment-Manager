using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM.UI.Utilities;
using System.Windows.Input;
using ViewModel.People;
using ViewModel.Room;
using Services.Interface;

namespace AM.UI.ViewModelUI
{
    internal class NavigationVM : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand RoomCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand TransactionsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();

        private void Customer(object obj) => CurrentView = new CustomerVM();

        private void Room(object obj) => CurrentView = new RoomVm();

        private void Order(object obj) => CurrentView = new OrderVM();

        private void Transaction(object obj) => CurrentView = new TransactionVM();

        private void Shipment(object obj) => CurrentView = new ShipmentVM();

        private void Setting(object obj) => CurrentView = new SettingVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            RoomCommand = new RelayCommand(Room);
            OrdersCommand = new RelayCommand(Order);
            TransactionsCommand = new RelayCommand(Transaction);
            ShipmentsCommand = new RelayCommand(Shipment);
            SettingsCommand = new RelayCommand(Setting);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}