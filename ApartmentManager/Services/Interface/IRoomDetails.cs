using Data.Entity;
using Data.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Room;
using ViewModel.RoomDetails;
using ViewModel.RoomImage;

namespace Services.Interface
{
    public interface IRoomDetails
    {

        Task<DeleteImageViewModel> Delete(int id);

        Task<bool> DeleteRoomFurniture(int id);

        Task<bool> CreateImage(RoomImageCreateViewModel request, string NameFile);
        Task<bool> CreateFurniture(RoomDetailsVm request);

        Task<List<CustomerVM>> GetAllCustomerInRoom(int IDroom);

        Task<PagedResult<RoomDetailsFurniture>> GetAllFurniture(RequestPaging request, int id);
        Task<PagedResult<RoomDetailsImage>> GetAllImage(RequestPaging request, int id);
    }
}