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

namespace AM.UI.State.Store
{
    public class BillStore
    {
        private readonly Apartment _apartment;
        private readonly List<BillVm> _billvm;
        private readonly IBill _ibill;
        private Lazy<Task> _initializeLazyBill;

        private event Action<BillVm> BillAdd;

        private event Action<BillVm> BillUpdate;

        private event Action<int> BillDelete;

        public List<BillVm> billvm => _billvm;

        public BillStore(Apartment apartment, IBill ibill)
        {
            _apartment = apartment;
            _ibill = ibill;
            _billvm = new List<BillVm>();
            _initializeLazyBill = new Lazy<Task>(InitializeBill);
        }

        private async Task InitializeBill()
        {
            List<BillVm> bill = await _apartment.GetAllBill();
            _billvm.Clear();
            _billvm.AddRange(bill);
        }

        public async Task LoadBill()
        {
            try
            {
                await _initializeLazyBill.Value;
            }
            catch (Exception)
            {
                _initializeLazyBill = new Lazy<Task>(InitializeBill);
                throw;
            }
        }

        public async Task<Bill> AddBill(BillCreateViewModel model)
        {
            var result = await _ibill.CreateBill(model);
            //MessageBox.Show(result.TotalMoney.ToString());
            BillVm create = new BillVm
            {
                ID = result.ID,
                IDRTC = model.Rental.IDRental,
                ElectricQuantity = result.ElectricQuantity,
                Active = result.Active,
                TotalMoney = result.TotalMoney,
                PayDate = result.PayDate,
            };
            _billvm.Add(create);
            BillAdd?.Invoke(create);
            return result;
        }

        public async Task<Bill> UpdateBill(BillUpdateViewModel model)
        {
            var result = await _ibill.UpdateBill(model);
            BillVm update = new BillVm
            {
                ID = result.ID,
                IDRTC = model.Rental.IDRental,
                ElectricQuantity = result.ElectricQuantity,
                Active = result.Active,
                PayDate = result.PayDate,
                TotalMoney = result.TotalMoney
            };
            var current = _billvm.FindIndex(x => x.ID == update.ID);
            if (current != -1)
            {
                _billvm[current] = update;
            }
            else _billvm.Add(update);
            BillUpdate?.Invoke(update);
            return result;
        }

        public async Task<bool> DeleteBill(int ID)
        {
            var result = await _ibill.DeleteBill(ID);
            _billvm.RemoveAll(x => x.ID == ID);
            BillDelete?.Invoke(ID);
            return result;
        }
    }
}