using AM.UI.Command.Home;
using AM.UI.State.Navigators;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.View.Dialog;
using AM.UI.ViewModelUI.Factory;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using ViewModel.Home;

namespace AM.UI.ViewModelUI
{
    public class HomeBillListingViewModel : ViewModelBase
    {
        private readonly IHome _Ihome;
        private readonly HomeStore _HomeStore;
        private readonly INavigator _navigator;
        private readonly IAparmentViewModelFactory _factory;

        private ObservableCollection<HomeItemVM> _listhome;

        public IEnumerable<HomeItemVM> listhome => _listhome;

        public DateTime now
        { get { return DateTime.Now; } }

        public UpdateElectricQuanlity requestUpdate { get; set; }

        public bool HasData => _listhome.Any();

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

        private bool _flag = false;

        public bool flag
        {
            get { return _flag; }
            set { _flag= value; OnPropertyChanged(nameof(flag)); }
        }

        private HomeItemVM _selectItem;

        public HomeItemVM selectItem
        {
            get
            { return _selectItem; }

            set
            {
                _selectItem = value;
                OnClickData();
            }
        }

        //Command
        public ICommand LoadDatabase { get; }

        public ICommand AddPE { get; }
        public ICommand ActiveCommand { get; }
        public ICommand AutoAddCommand { get; }
        public ICommand EditElectric { get; }
        public ICommand ConfirmUpdate { get; }
        public ICommand TestClick { get; }

        //Command
        public HomeBillListingViewModel(IHome ihome, HomeStore homeStore, INavigator navigator, IAparmentViewModelFactory factory)
        {
            _Ihome=ihome;
            _HomeStore=homeStore;
            _navigator=navigator;
            _factory=factory;
            _listhome = new ObservableCollection<HomeItemVM>();
            _selectItem = new HomeItemVM();
            LoadDatabase = new LoadBillListingCommand(this, homeStore);
            AutoAddCommand = new AutoAddBillCommand(this, homeStore);
            ConfirmUpdate = new EditElectricCommand(this, homeStore);
            AddPE = new AddPaymentCommand(this, homeStore);
            EditElectric = new RelayCommand(EditElec);
            ActiveCommand = new EditActiveCommand(this, homeStore);
            TestClick = new RelayCommand(DisposeDataCommand);
            LoadDatabase.Execute(null);
            _HomeStore.AddPaymentStore +=AddPaymentHome_Store;
            _HomeStore.HomeAdd += Store_Add;
            _HomeStore.EventDeleteBillListtingPayment +=EventDeleteBillListtingPayment;
            _HomeStore.EventEditActive+=testthu;
            _listhome.CollectionChanged += OnReservationsChanged;
        }

        public void EventDeleteBillListtingPayment(List<HomeItemVM> itemVM)
        {
            _listhome.Clear();
            foreach (var item in itemVM)
            {
                _listhome.Add(item);
            }
        }

        public void AddPaymentHome_Store(int id)
        {
            var object1 = _listhome.FirstOrDefault(y => y.ID == id);

            if (object1 != null)
            {
                _listhome.Remove(object1);
            }
            DisposeDataCommand(null);
        }

        public void DisposeDataCommand(object parameter)
        {
            selectItem = null;
            flag = false;
        }

        protected override void Dispose()
        {
            _HomeStore.AddPaymentStore -=AddPaymentHome_Store;
            _HomeStore.HomeAdd -= Store_Add;
            _HomeStore.EventDeleteBillListtingPayment -= EventDeleteBillListtingPayment;
            _HomeStore.EventEditActive -= testthu;
            base.Dispose();
        }

        public void LoadHomeVM(List<HomeItemVM> list)
        {
            _listhome.Clear();
            foreach (var item in list)
            {
                _listhome.Add(item);
            }
        }

        public void testthu(HomeItemVM items)
        {
            var currentIndex = _listhome.ToList().FindIndex(x => x.IsActive == items.IsActive);
            if (currentIndex != -1)
            {
                _listhome[currentIndex] = items;
            }
            else
            {
                _listhome.Add(items);
            }
            DisposeDataCommand(null);
        }

        public void EditElec(object parameter)
        {
            if (parameter is HomeItemVM model)
            {
                MessageBoxCustom message = new MessageBoxCustom("", MessageType.Input, MessageButtons.YesNo);
                message.ShowDialog();
                if (!message.Input.IsNullOrEmpty())
                {
                    requestUpdate = new UpdateElectricQuanlity
                    {
                        Id = model.ID,
                        ElectricMoneyDefualt = model.ElectricMoney,
                        Quality = Convert.ToInt32(message.Input),
                        RoomMoney = model.RoomMoney,
                        WaterMoney = model.WaterMoney,
                        ServiceMoney = model.ServiceMoney,
                        Active = model.Active,
                        TotalMoney = (Convert.ToInt32(message.Input) * model.ElectricMoney)+ model.RoomMoney+ model.WaterMoney+model.ServiceMoney
                    };
                    model.ElecQuality = Convert.ToInt32(message.Input);
                    model.TotalMoney = (model.ElecQuality * model.ElectricMoney)+ model.RoomMoney+ model.WaterMoney+model.ServiceMoney;
                    ConfirmUpdate.Execute(requestUpdate);
                }
            }
        }

        private void EditElectricStore(HomeItemVM Data)
        {
            var currentIndex = _listhome.ToList().FindIndex(x => x.ID == Data.ID);

            if (currentIndex != -1)
            {
                MessageBox.Show(Data.TotalMoney.ToString());

                _listhome[currentIndex] = Data;
            }
            else
            {
                _listhome.Add(Data);
            }
        }

        private void Store_Add(HomeItemVM Data)
        {
            _listhome.Add(Data);
        }

        private void OnReservationsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(HasData));
        }

        private void OnClickData()
        {
            flag =true;
            OnPropertyChanged(nameof(selectItem));
        }
    }
}