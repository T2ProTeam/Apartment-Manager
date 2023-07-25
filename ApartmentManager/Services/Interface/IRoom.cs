using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Room;
using ViewModel.RoomImage;

namespace Services.Interface
{
    public interface IRoom
    {
        Task<int> CreateRoom(RoomCreateViewModel model);

        Task<int> UpdateRoom(RoomUpdateViewModel model);

        Task<int> DeleteRoom(int ImageId);

        Task<PagedResult<RoomVm>> GetAllPage(RequestPaging request);
    }
}