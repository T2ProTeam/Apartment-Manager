using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Room;

namespace Services.Interface
{
    public interface IFurniture
    {
        Task<Furniture> CreateFurniture(FurnitureCreateViewModel request);

        Task<Furniture> UpdateFurniture(FurnitureUpdateViewModel model);

        Task<bool> DeleteFurniture(int FurnitureId);

        Task<PagedResult<FurnitureVm>> GetAllPage(RequestPaging request);

        Task<List<FurnitureVm>> GetAll();
    }
}