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
using AM.UI.View.Rooms;
using UI.View.Homes;
using Data.Entity;

namespace AM.UI.ViewModelUI
{
    public class NavigationVM : ViewModelBase
    {
        private readonly RoomVMUI _roomVMUI;
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }




        public ICommand HomeCommand { get; set; }
        public ICommand RoomCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SubmitCommand { get; set; }

        private void RoomAdd(object obj) => CurrentView = new RoomAdd();
        private void Home(object obj) => CurrentView = new HomeVM();


        private void Room(object obj) => CurrentView = new RoomHome();

 

        public NavigationVM()
        {

            HomeCommand = new RelayCommand(Home);
            RoomCommand = new RelayCommand(Room);
            AddCommand = new RelayCommand(RoomAdd);
            SubmitCommand = new RelayCommand(Room);

            // Startup Page
            CurrentView = new HomeVM();
        } 
    }
}