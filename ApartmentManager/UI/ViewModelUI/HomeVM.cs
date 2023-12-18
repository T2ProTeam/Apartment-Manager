using AM.UI.Command;
using AM.UI.Command.Home;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using Data.Enum;
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
using ViewModel.Home;
using ViewModel.People;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ViewModel.Room;
using AM.UI.View.Dialog;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MaterialDesignThemes.Wpf;
using Microsoft.IdentityModel.Abstractions;
using AM.UI.ViewModelUI.Factory;

namespace AM.UI.ViewModelUI
{
    public class HomeVM : ViewModelBase
    {
        //readonly
        private readonly INavigator _navigator;

        private readonly IAparmentViewModelFactory _factory;
        private readonly IHome _Ihome;
        private readonly HomeStore _HomeStore;
        public ViewModelBase CurrentHomeViewModel => _navigator.CurrentHomeViewModel;
        //readonly

        //object
        public DateTime now
        { get { return DateTime.Now; } }

        //object
        //command

        public ICommand Loaddata { get; }
        public ICommand LoadCurrentHomeVM { get; }

        //command
        //viewmodel
        private NumberOfHomeVM _NumberOfHomeVM;

        public NumberOfHomeVM NumberOfHomeVM
        {
            get { return _NumberOfHomeVM; }
            set
            {
                _NumberOfHomeVM = value;
                OnPropertyChanged(nameof(NumberOfHomeVM));
            }
        }

        //viewmodel

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

        public bool IsConfirm { get; set; }

        public HomeVM(INavigator navigator, IHome home, HomeStore homeStore, IAparmentViewModelFactory aparmentViewModelFactory)
        {
            _HomeStore = homeStore;
            _navigator = navigator;
            _Ihome = home;
            _factory = aparmentViewModelFactory;
            _navigator.StateChanged += Navigator_StateChanged;
            Loaddata = new LoadHomeCommand(home, this);
            LoadCurrentHomeVM = new UpdateCurrentHomeViewModelCommand(home, homeStore, navigator, aparmentViewModelFactory);
            Loaddata.Execute(null);
            LoadCurrentHomeVM.Execute(ViewHomeType.Bill);
            _HomeStore.AddPaymentStore += AddPayment_Store;
            _HomeStore.EventDeleteNumberPayment += EventDeletePayment_Store;
        }

        private void EventDeletePayment_Store()
        {
            Loaddata.Execute(null);
            NumberOfHomeVM.NumberPE = NumberOfHomeVM.NumberPE-1;
        }

        private void AddPayment_Store(int id)
        {
            Loaddata.Execute(null);
            NumberOfHomeVM.NumberPE +=1;
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentHomeViewModel));
        }
    }

    public enum ViewHomeType
    {
        Bill,
        Customer,
        EmptyRoom,
        Payment
    }
}