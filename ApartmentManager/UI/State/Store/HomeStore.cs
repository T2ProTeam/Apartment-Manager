using AM.UI.Model;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Bill;
using ViewModel.Home;
using ViewModel.PaymentExtension;
using ViewModel.People;
using ViewModel.Room;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AM.UI.State.Store
{
    public class HomeStore
    {
        //privatereaddon
        private readonly List<HomeItemVM> homeItemVMs;

        private readonly List<CustomerVM> _HomeCustomerListingVM;
        private readonly List<RoomVm> _HomeEmptyRoomVM;
        private readonly List<PaymentExtensionVm> _HomePEVM;
        private readonly IHome _Ihome;
        private readonly IPeople _Ipeople;
        private readonly IRoom _Iroom;
        private readonly IPaymentExtension _Ipayment;

        //privatereaddon
        public List<HomeItemVM> HomeItemVMs => homeItemVMs;

        public List<CustomerVM> HomeCustomerListingVM => _HomeCustomerListingVM;
        public List<RoomVm> HomeEmptyRoomVM => _HomeEmptyRoomVM;
        public List<PaymentExtensionVm> HomePEVM => _HomePEVM;

        private Lazy<Task> _initialLazyListingBill;
        private Lazy<Task> _initialLazyListingCustomer;
        private Lazy<Task> _initialLazyListingEmptyRoom;
        private Lazy<Task> _initialLazyListingPayment;

        public event Action<HomeItemVM> HomeAdd;

        public event Action<HomeItemVM> HomeUpdateElectric;

        public event Action<int> AddPaymentStore;

        public event Action<PaymentExtensionCreateViewModel> UpdatePaymentStore;

        public event Action<int> EventDeletePaymentStore;

        public event Action EventDeleteNumberPayment;

        public event Action<List<HomeItemVM>> EventDeleteBillListtingPayment;

        public event Action<HomeItemVM> EventEditActive;

        public DateTime date { get; set; }

        public HomeStore(IHome home, IPeople ipeople, IRoom iroom, IPaymentExtension ipayment)
        {
            date = DateTime.Now;
            _Ihome = home;
            _Ipeople=ipeople;
            _Iroom=iroom;
            _Ipayment=ipayment;
            homeItemVMs = new List<HomeItemVM>();
            _HomeCustomerListingVM = new List<CustomerVM>();
            _HomeEmptyRoomVM = new List<RoomVm>();
            _HomePEVM = new List<PaymentExtensionVm>();
            _initialLazyListingBill = new Lazy<Task>(InitializeBill);
            _initialLazyListingCustomer = new Lazy<Task>(InitializeCustomer);
            _initialLazyListingEmptyRoom = new Lazy<Task>(InitializeEmptyRoom);
            _initialLazyListingPayment = new Lazy<Task>(InitializePayment);
        }

        //load database
        private async Task InitializeBill()
        {
            List<HomeItemVM> bill = await _Ihome.GetDataBase(date);
            homeItemVMs.Clear();
            homeItemVMs.AddRange(bill);
        }

        public async Task LoadBill(DateTime request)
        {
            try
            {
                date =request;
                await _initialLazyListingBill.Value;
            }
            catch (Exception)
            {
                _initialLazyListingBill = new Lazy<Task>(InitializeBill);
                throw;
            }
        }

        private async Task InitializeCustomer()
        {
            List<CustomerVM> customers = await _Ipeople.GetAll();
            _HomeCustomerListingVM.Clear();
            _HomeCustomerListingVM.AddRange(customers);
        }

        public async Task LoadCustomer()
        {
            try
            {
                await _initialLazyListingCustomer.Value;
            }
            catch (Exception)
            {
                _initialLazyListingCustomer = new Lazy<Task>(InitializeCustomer);
                throw;
            }
        }

        private async Task InitializeEmptyRoom()
        {
            List<RoomVm> emptyrooms = await _Iroom.GetAllEmptyRom();
            _HomeEmptyRoomVM.Clear();
            _HomeEmptyRoomVM.AddRange(emptyrooms);
        }

        public async Task LoadEmptyRoom()
        {
            try
            {
                await _initialLazyListingEmptyRoom.Value;
            }
            catch (Exception)
            {
                _initialLazyListingEmptyRoom = new Lazy<Task>(InitializeEmptyRoom);
                throw;
            }
        }

        private async Task InitializePayment()
        {
            List<PaymentExtensionVm> PEs = await _Ipayment.GetAllDate(date);
            _HomePEVM.Clear();
            _HomePEVM.AddRange(PEs);
        }

        public async Task LoadPayment()
        {
            try
            {
                await _initialLazyListingPayment.Value;
            }
            catch (Exception)
            {
                _initialLazyListingPayment = new Lazy<Task>(InitializePayment);
                throw;
            }
        }

        //load database

        public async Task<int> AutoAddBill(AutoAddHomeVM request)
        {
            var result = await _Ihome.AutoAdd(request);
            if (result.items!=null)
            {
                foreach (var itemVM in result.items)
                {
                    homeItemVMs.Add(itemVM);
                    HomeAdd?.Invoke(itemVM);
                }
            }
            return result.result;
        }

        public async Task<Bill> EditActive(HomeItemVM request)
        {
            var result = await _Ihome.updateActive(request);
            EventEditActive?.Invoke(request);
            return result;
        }

        public async Task<int> AddPayment(PaymentExtensionCreateViewModel request)
        {
            PaymentExtension result = await _Ipayment.CreatePaymentExtension(request);
            int flag = 0;
            if (result!=null)
            {
                flag=1;
                request.ID = result.ID;
                homeItemVMs.RemoveAll(a => a.ID == request.IDBill);

                HomePEVM.Add(new PaymentExtensionVm
                {
                    ID= result.ID,
                    IDBill = result.IDBill,
                    Days= result.Days
                });
                AddPaymentStore?.Invoke(request.IDBill);
                UpdatePaymentStore?.Invoke(request);
            }

            return flag;
        }

        public async Task<Bill> UpdateElectric(UpdateElectricQuanlity request)
        {
            var result = await _Ihome.updateElectric(request);

            return result;
        }

        public async Task<int> DeletePayment(int ID, int IDBill)
        {
            var result = await _Ipayment.DeletePaymentExtension(ID);
            var HomeBillItem = await _Ihome.GetDataBase(date);
            if (result==1)
            {
                homeItemVMs.Clear();
                HomeBillItem.ForEach(x => homeItemVMs.Add(x));
                _HomePEVM.RemoveAll(x => x.ID==ID);
                EventDeleteNumberPayment?.Invoke();
                EventDeleteBillListtingPayment?.Invoke(HomeBillItem);
                EventDeletePaymentStore?.Invoke(ID);
            }
            return result;
        }
    }
}