using AM.UI.Model;
using Caliburn.Micro;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.DepositsContract;

namespace AM.UI.State.Store
{
    public class DepositContractStore
    {
        private readonly Apartment _apartment;
        private readonly List<DepositsContractVm> _depositsvm;
        private readonly IDepositsContract _ideposit;
        private Lazy<Task> _initialLazyDeposits;
        public List<DepositsContractVm> depositsvm => _depositsvm;

        public event Action<DepositsContractVm> DepositCreate;

        public event Action<DepositsContractVm> DepositUpdate;

        public event Action<DepositsContractVm> LoadRoomVmWhenCreateDeposit;

        public event Action<int> DepositDelete;

        public DepositContractStore(Apartment apartment, IDepositsContract ideposit)
        {
            _apartment = apartment;
            _ideposit = ideposit;
            _depositsvm = new List<DepositsContractVm>();
            _initialLazyDeposits = new Lazy<Task>(InitializeDepositsContract);
        }

        private async Task InitializeDepositsContract()
        {
            List<DepositsContractVm> deposit = await _apartment.GetAllDepositContract();
            _depositsvm.Clear();
            _depositsvm.AddRange(deposit);
        }

        public async Task LoadDepositsContract()
        {
            try
            {
                await _initialLazyDeposits.Value;
            }
            catch (Exception)
            {
                _initialLazyDeposits = new Lazy<Task>(InitializeDepositsContract);
                throw;
            }
        }

        public async Task<DepositsContract> CreateDeposit(DepositsContractCreateViewModel model)
        {
            var result = await _ideposit.CreateDepositsContract(model);
            DepositsContractVm deposit = new DepositsContractVm
            {
                ID = model.ID,
                RoomName = model.Room.Name,
                LeaderName = model.Customer.Name,
                DepositsDate = model.DepositsDate,
                ReceiveDate = model.ReceiveDate,
                CheckOutDate = model.CheckOutDate,
                Money = model.Money
            };
            _depositsvm.Add(deposit);
            DepositCreate?.Invoke(deposit);
            LoadRoomVmWhenCreateDeposit?.Invoke(deposit);
            return result;
        }

        public async Task<DepositsContract> UpdateDeposit(DepositsContractUpdateViewModel model)
        {
            var result = await _ideposit.UpdateDepositsContract(model);
            DepositsContractVm deposit = new DepositsContractVm
            {
                ID = model.ID,
                RoomName = model.Room.Name,
                DepositsDate = model.DepositsDate,
                ReceiveDate = model.ReceiveDate,
                CheckOutDate = model.CheckOutDate,
                Money = model.Money
            };
            var current = _depositsvm.FindIndex(x => x.ID == result.ID);
            if (current != -1)
            {
                _depositsvm[current] = deposit;
            }
            else
            {
                _depositsvm.Add(deposit);
            }
            DepositUpdate?.Invoke(deposit);
            return result;
        }

        public async Task<bool> DeleteDeposit(int ID)
        {
            var result = await _ideposit.DeleteDepositsContract(ID);
            _depositsvm.RemoveAll(x => x.ID == ID);
            DepositDelete?.Invoke(ID);
            return result;
        }
    }
}