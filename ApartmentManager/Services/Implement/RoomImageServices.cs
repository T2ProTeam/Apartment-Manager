using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Room;
using ViewModel.RoomImage;

namespace Services.Implement
{
    public class RoomImageServices : IRoomImage
    {
        public Task<int> CreateImage(RoomImageCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteImage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<RoomImageVm>> GetAllPage(RequestPaging request)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImage(RoomImageUpdateViewModel model)
        {
            throw new NotImplementedException();
        }

        Task<RoomImage> IRoomImage.CreateImage(RoomImageCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        Task<PagedResult<RoomVm>> IRoomImage.GetAllPage(RequestPaging request)
        {
            throw new NotImplementedException();
        }
    }
}
