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
using ViewModel.People;

namespace AM.UI.ViewModelUI
{
    public class HomeViewDetailCustomerVMUI : ViewModelBase
    {
        private readonly HomeStore _HomeStore;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;

        private ObservableCollection<CustomerVM> _ListCustomer;
        public IEnumerable<CustomerVM> ListCustomer => _ListCustomer;
        public bool HasData => _ListCustomer.Any();

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
        public ICommand LoadDataCommand { get; }

        public HomeViewDetailCustomerVMUI(IHome home, HomeStore homeStore, INavigator navigator, IAparmentViewModelFactory aparmentViewModelFactory)
        {
            _HomeStore = homeStore;
            _navigator = navigator;
            _factory = aparmentViewModelFactory;
            _ListCustomer = new ObservableCollection<CustomerVM>();
            ReturnBillHomeVM = new UpdateCurrentHomeViewModelCommand(home, homeStore, navigator, aparmentViewModelFactory);
            LoadDataCommand = new LoadCustomerListtingCommand(this, homeStore);
            LoadDataCommand.Execute(null);
            _ListCustomer.CollectionChanged += OnReservationsChanged;
        }

        public void UpdateData(List<CustomerVM> data)
        {
            _ListCustomer.Clear();
            foreach (var item in data)
            {
                _ListCustomer.Add(item);
            }
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }
    }
}