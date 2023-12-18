using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.DepositsContract;
using ViewModel.Dtos;
using ViewModel.People;

namespace Services.Interface
{
    public interface IDepositsContract
    {
        Task<DepositsContract> CreateDepositsContract(DepositsContractCreateViewModel model);

        Task<DepositsContract> UpdateDepositsContract(DepositsContractUpdateViewModel model);

        Task<bool> DeleteDepositsContract(int depositsId);

        Task<PagedResult<DepositsContractVm>> GetAllPage(RequestPaging request);
    }
}