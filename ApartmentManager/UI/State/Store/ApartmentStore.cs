using AM.UI.Command;
using AM.UI.Model;
using Data;
using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Bill;
using ViewModel.DepositsContract;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.PaymentExtension;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;
using ViewModel.RoomDetails;
using ViewModel.RoomImage;

namespace AM.UI.State
{
    public class ApartmentStore
    {
        private readonly Apartment _apartment;
        private readonly List<CustomerVM> _customervm;

        private readonly IPeople _people;

        private Lazy<Task> _initializeLazy;

        public List<CustomerVM> customervm => _customervm;

        public event Action<int> YouTubeViewerDeleted;

        public event Action<CustomerVM> CustomerAdd;

        public event Action<CustomerVM> CustomerUpdate;

        public event Action<List<CustomerVM>> CreatePeopleInRental;

        public ApartmentStore(Apartment apartment, IPeople people, IRoom room, IRoomDetails roomimage)
        {
            _apartment = apartment;
            _people = people;

            _initializeLazy = new Lazy<Task>(Initialize);

            _customervm = new List<CustomerVM>();
        }

        //----------------------------------------Customer
        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        private async Task Initialize()
        {
            List<CustomerVM> customer = await _apartment.GetAllcustomer();
            _customervm.Clear();
            _customervm.AddRange(customer);
        }

        public async Task<People> AddCustomer(PeopleCreateViewModel request)
        {
            var result = await _people.Create(request);
            CustomerVM create = new CustomerVM
            {
                ID = result.ID,
                //IDroom = request.IDroom,
                Name = request.Name,
                Sex = request.Sex,
                Birthday= request.Birthday,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IDCard = request.IDCard,
                Address = request.Address,
            };
            _customervm.Add(create);
            CustomerAdd?.Invoke(create);
            return result;
        }

        public async Task<People> UpdateCustomer(PeopleUpdateViewModel request)
        {
            var result = await _people.Edit(request.ID, request);
            CustomerVM create = new CustomerVM
            {
                ID = result.ID,
                //IDroom = request.IDroom,
                Name = request.Name,
                Sex = request.Sex,
                Birthday= request.Birthday,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IDCard = request.IDCard,
                Address = request.Address,
            };
            int currentIndex = _customervm.FindIndex(y => y.ID == create.ID);

            if (currentIndex != -1)
            {
                _customervm[currentIndex] = create;
            }
            else
            {
                _customervm.Add(create);
            }
            CustomerUpdate?.Invoke(create);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _people.Delete(id);

            _customervm.RemoveAll(y => y.ID == id);

            YouTubeViewerDeleted?.Invoke(id);
            return result;
        }

        public async Task<int> GetlastIDpeople()
        {
            var result = await _people.GetLast();
            return result;
        }

        public async Task<int> CreateManyCustomer(List<PeopleCreateViewModel> request)
        {
            var lastid = await GetlastIDpeople();
            var result = await _people.Createmany(request);
            List<CustomerVM> resultList = new List<CustomerVM>();

            request.ForEach(async x =>
            {
                lastid++;
                var findid = await _people.GetByID(lastid);
                CustomerVM add = new CustomerVM
                {
                    ID= lastid,
                    RoomName = findid.RoomName,
                    Name = findid.Name,
                    Sex = findid.Sex,
                    Birthday= findid.Birthday,
                    PhoneNumber = findid.PhoneNumber,
                    Email = findid.Email,
                    IDCard = findid.IDCard,
                    Address = findid.Address,
                };
                resultList.Add(add);
            });

            _customervm.AddRange(resultList);
            CreatePeopleInRental?.Invoke(resultList);
            return result;
        }
    }
}