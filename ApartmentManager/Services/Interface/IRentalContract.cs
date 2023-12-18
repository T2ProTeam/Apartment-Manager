using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;

namespace Services.Interface
{
    public interface IRentalContract
    {
        Task<List<RentalContractVm>> GetAll();

        Task<RentalContract> Create(RentalContractCreateViewModel model);

        Task<RentalContract> Update(RentalContractUpdateViewModel model);

        Task<List<RentalContractForCombobox>> GetAllRental();


        Task<bool> Delete(int id);

        Task<RentalContract> GetById(int id);

        Task<PagedResult<RentalContractVm>> GetAllPage(RequestPaging request);
    }
}