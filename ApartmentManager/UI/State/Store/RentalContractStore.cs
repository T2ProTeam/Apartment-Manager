using AM.UI.Model;
using AM.UI.View.RentalContract;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.People;
using ViewModel.RentalContract;

namespace AM.UI.State.Store
{
    public class RentalContractStore
    {
        private readonly Apartment _apartment;
        private readonly List<RentalContractVm> _rentalvm;
        private readonly IRentalContract _irental;
        private readonly IPeople _Ipeople;
        private readonly ApartmentStore _apartmentStore;
        public List<RentalContractVm> rentalvm => _rentalvm;

        private Lazy<Task> _initialLazyRental;

        public event Action<RentalContractVm> RentalContractCreate;

        public event Action<RentalContractVm> RentalContractUpdate;

        public event Action<int> RentalContractDelete;

        public RentalContractStore(Apartment apartment, ApartmentStore apartmentStore, IPeople people, IRentalContract irental)
        {
            _irental = irental;
            _Ipeople = people;
            _apartment = apartment;
            _apartmentStore = apartmentStore;
            _rentalvm = new List<RentalContractVm>();
            _initialLazyRental = new Lazy<Task>(InitializeRentalContract);
        }

        private async Task InitializeRentalContract()
        {
            List<RentalContractVm> rental = await _apartment.GetAllRentalContract();
            _rentalvm.Clear();
            _rentalvm.AddRange(rental);
        }

        public async Task LoadeRentalContract()
        {
            try
            {
                await _initialLazyRental.Value;
            }
            catch (Exception)
            {
                _initialLazyRental = new Lazy<Task>(InitializeRentalContract);
                throw;
            }
        }

        public async Task<RentalContract> CreateRentalContract(RentalContractCreateViewModel model)
        {
            var result = await _irental.Create(model);
            RentalContractVm rental = new RentalContractVm
            {
                ID = result.ID,
                RoomName = model.RoomCombobox.Name,
                ReceiveDate = result.ReceiveDate,
                CheckOutDate = result.CheckOutDate,
                RoomMoney = result.RoomMoney,
                ElectricMoney = result.ElectricMoney,
                WaterMoney = result.WaterMoney,
                ServiceMoney = result.ServiceMoney
            };
            _rentalvm.Add(rental);
            RentalContractCreate?.Invoke(rental);
            return result;
        }

        public async Task<RentalContract> UpdateRentalContract(RentalContractUpdateViewModel model)
        {
            var result = await _irental.Update(model);
            RentalContractVm rental = new RentalContractVm
            {
                ID = result.ID,
                RoomName = model.RoomCombobox.Name,
                LeaderName = model.CustomerCombobox.Name,
                ReceiveDate = result.ReceiveDate,
                CheckOutDate = result.CheckOutDate,
                RoomMoney = result.RoomMoney,
                ElectricMoney = result.ElectricMoney,
                WaterMoney = result.WaterMoney,
                ServiceMoney = result.ServiceMoney,
                Active = result.Active
            };
            var current = _rentalvm.FindIndex(x => x.ID == result.ID);
            if (current != -1)
            {
                _rentalvm[current] = rental;
            }
            else
            {
                _rentalvm.Add(rental);
            }
            RentalContractUpdate?.Invoke(rental);
            return result;
        }

        public async Task<bool> DeleteRentalContract(int id)
        {
            var result = await _irental.Delete(id);
            _rentalvm.RemoveAll(x => x.ID == id);
            RentalContractDelete?.Invoke(id);
            return result;
        }

        public async Task<int> GetlastIDpeople()
        {
            var result = await _apartmentStore.GetlastIDpeople();
            return result;
        }

        public async Task<int> CreateManyCustomer(List<PeopleCreateViewModel> request)
        {
            var result = await _apartmentStore.CreateManyCustomer(request);
            return result;
        }
    }
}