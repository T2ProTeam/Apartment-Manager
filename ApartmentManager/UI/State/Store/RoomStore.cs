using AM.UI.Model;
using Data.Entity;
using Data.Relationships;
using Microsoft.Identity.Client;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;
using ViewModel.RoomDetails;
using ViewModel.RoomImage;

namespace AM.UI.State.Store
{
    public class RoomStore
    {
        private readonly Apartment _apartment;

        private readonly List<RoomVm> _roomvm;
        private readonly List<CustomerVM> _customerVM;

        private readonly IRoom _room;
        private readonly IRoomDetails _roomdetails;
        private readonly IFurniture _ifur;
        private readonly IRentalContract _irental;
        private readonly IPeople _ipeople;

        private Lazy<Task> _initializeLazyRoom;
        public List<RoomVm> roomvm => _roomvm;
        public List<CustomerVM> customerVM => _customerVM;

        private readonly List<RoomDetailsFurniture> _roomdetailsvmfur;

        private readonly List<RoomDetailsVm> _furniturecreatevm;
        private readonly List<RoomImageCreateViewModel> _roomimagevms;

        public List<RoomDetailsFurniture> roomdetailsvmfur => _roomdetailsvmfur;
        public List<RoomImageCreateViewModel> roomimagevms => _roomimagevms;
        public List<RoomDetailsVm> furniturecreatevm => _furniturecreatevm;

        public event Action<RoomVm> RoomAdd;

        public event Action<RoomVm> RoomUpdate;

        public event Action<RoomVm> LoadAgainForRentalContract;

        public event Action<RoomVm> LoadAgainForDepositContract;

        public event Action<int> RoomDelete;

        public event Action<RoomImageCreateViewModel> RoomImageAdd;

        public event Action<int> RoomImageDelete;

        public event Action<RoomDetailsFurniture> RoomFurnitureCreate;

        public event Action<int> RoomFurnitureDelete;

        public RoomStore(Apartment apartment, IRoom room, IRoomDetails roomimages, IFurniture ifur, IRentalContract irental, IPeople ipeople)
        {
            _apartment = apartment;
            _roomvm = new List<RoomVm>();
            _customerVM = new List<CustomerVM>();
            _room = room;
            _roomdetails = roomimages;
            _roomimagevms = new List<RoomImageCreateViewModel>();
            _roomdetailsvmfur = new List<RoomDetailsFurniture>();
            _furniturecreatevm = new List<RoomDetailsVm>();
            _initializeLazyRoom = new Lazy<Task>(InitializeRoom);
            _ifur = ifur;
            _irental = irental;
            _ipeople = ipeople;
        }

        public async Task LoadRoom()
        {
            try
            {
                await _initializeLazyRoom.Value;
            }
            catch (Exception)
            {
                _initializeLazyRoom = new Lazy<Task>(InitializeRoom);
                throw;
            }
        }

        private async Task InitializeRoom()
        {
            List<RoomVm> room = await _apartment.GettAllRoom();
            _roomvm.Clear();
            _roomvm.AddRange(room);
        }

        public async Task<Room> AddRoom(RoomCreateViewModel request)
        {
            var result = await _room.Create(request);
            RoomVm create = new RoomVm
            {
                ID = result.ID,
                Name = result.Name,
                Quantity = result.Quantity,
            };
            _roomvm.Add(create);
            RoomAdd?.Invoke(create);
            return result;
        }

        public async Task<Room> UpdateRoom(RoomUpdateViewModel request)
        {
            var result = await _room.Update(request);
            RoomVm update = new RoomVm
            {
                ID = result.ID,
                Name = result.Name,
                Quantity = result.Quantity,
                Staked = result.Staked,
            };

            var currentIndex = _roomvm.FindIndex(x => x.ID == result.ID);
            if (currentIndex != -1)
            {
                _roomvm[currentIndex] = update;
            }
            else
            {
                _roomvm.Add(update);
            }
            RoomUpdate?.Invoke(update);
            LoadAgainForRentalContract?.Invoke(update);
            LoadAgainForDepositContract?.Invoke(update);
            return result;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var result = await _room.Delete(id);
            _roomvm.RemoveAll(x => x.ID == id);
            RoomDelete?.Invoke(id);
            return result;
        }

        public async Task<bool> CreateImage(RoomImageCreateViewModel request, string NameFile)
        {
            var result = await _roomdetails.CreateImage(request, NameFile);
            RoomImageCreateViewModel create = new RoomImageCreateViewModel
            {
                ID = request.ID,
                IDroom = request.IDroom,
                Name = request.Name,
                Url = NameFile,
            };
            _roomimagevms.Add(create);
            RoomImageAdd?.Invoke(create);
            return result;
        }

        public async Task<bool> CreateFurniture(RoomDetailsVm request)
        {
            var result = await _roomdetails.CreateFurniture(request);
            RoomDetailsFurniture create = new RoomDetailsFurniture
            {
                IdFur = request.IDFur,
                IdRoom = request.IDRoom
            };
            _roomdetailsvmfur.Add(create);
            RoomFurnitureCreate?.Invoke(create);
            return result;
        }

        public async Task<bool> DeleteFurniture(int id)
        {
            var result = await _roomdetails.DeleteRoomFurniture(id);
            _roomdetailsvmfur.RemoveAll(x => x.IdFur == id);
            RoomFurnitureDelete?.Invoke(id);
            return result;
        }

        public async Task<int> DeteleImage(int id)
        {
            var result = await _roomdetails.Delete(id);

            _roomimagevms.RemoveAll(y => y.ID == id);
            RoomImageDelete?.Invoke(id);

            return 1;
        }

        public async Task<List<CustomerVM>> GetAllCustomerInRoomByIDRoom(int IDroom)
        {
            return await _roomdetails.GetAllCustomerInRoom(IDroom);
        }

        public async Task<List<RoomDetailsImage>> LoadRoomDetailsImage(int id)
        {
            List<RoomDetailsImage> roomdetailsImage = await _apartment.GetAllRoomDetailsImage(id);
            return roomdetailsImage;
        }

        public async Task<List<RoomDetailsFurniture>> LoadRoomDetailsFurniture(int id)
        {
            List<RoomDetailsFurniture> roomdetails = await _apartment.GetAllRoomDetailsFurniture(id);
            return roomdetails;
        }

        public async Task<List<FurnitureVm>> LoadFurniture()
        {
            return await _ifur.GetAll();
        }

        public async Task<List<CustomerForCombobox>> LoadCustomerForCombobox()
        {
            return await _ipeople.GetIdNameForCombobox();
        }

        public async Task<List<RoomVm>> LoadInformationRoom()
        {
            return await _room.GetAll();
        }
    }
}