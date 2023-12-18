using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Bill;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Room;

namespace Services.Interface
{
    public interface IBill
    {
        Task<Bill> CreateBill(BillCreateViewModel model);

        Task<Bill> UpdateBill(BillUpdateViewModel model);

        Task<int> UpdateActiveBill(int model);

        Task<bool> DeleteBill(int ID);

        Task<PagedResult<BillVm>> GetAllPage(RequestPaging request);
    }
}