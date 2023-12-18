using AM.UI.Command.LoadDataBase;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI.Factory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ViewModel.People;
using ViewModel.Room;

namespace AM.UI.ViewModelUI.RoomDetails
{
    public class RoomDetailsInformationCustomerVMUI : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _viewmodel;
        private readonly RoomStore _store;
        private ObservableCollection<CustomerVM> _Customer;
        public IEnumerable<CustomerVM> Customer => _Customer;


        private RoomVm _roomvm;
        public RoomVm roomvm
        {
            get { return _roomvm; }
            set
            {
                roomvm = value;
                OnPropertyChanged(nameof(RoomVm));
            }
        }

        public ICommand check { get; }

        public ICommand LoadListViewForNameCustomer { get; }

        public RoomDetailsInformationCustomerVMUI(INavigator navigator, IAparmentViewModelFactory viewmodel, RoomStore store, RoomVm r)
        {
            _navigator = navigator;
            _viewmodel = viewmodel;
            _store = store;
            _roomvm = r;
            _Customer = new ObservableCollection<CustomerVM>();
            check = new RelayCommand(checkbutton);
            LoadListViewForNameCustomer = new LoadInformationCustomerForRoomDetails(_store, this, r.ID);
            LoadListViewForNameCustomer.Execute(null);
        }
        
        public void checkbutton(object parameter)
        {
            MessageBox.Show(_Customer.Count().ToString());
        }


        public void TakeNameForListView(List<CustomerVM> data)
        {
            data.ForEach(x => _Customer.Add(x));
        }
    }
}
