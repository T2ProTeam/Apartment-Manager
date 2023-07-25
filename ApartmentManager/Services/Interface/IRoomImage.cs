using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.RoomImage;

namespace Services.Interface
{
    public interface IRoomImage
    {
        Task<int> CreateImage(RoomImageCreateViewModel model);

        Task<int> UpdateImage(RoomImageUpdateViewModel model);

        Task<int> DeleteImage(int id);

        Task<PagedResult<RoomImageVm>> GetAllPage(RequestPaging request);
    }
}