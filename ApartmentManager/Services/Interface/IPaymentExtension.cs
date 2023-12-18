using Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.PaymentExtension;
using ViewModel.People;

namespace Services.Interface
{
    public interface IPaymentExtension
    {
        Task<List<PaymentExtensionVm>> GetAll();

        Task<List<PaymentExtensionVm>> GetAllDate(DateTime date);

        Task<PaymentExtension> CreatePaymentExtension(PaymentExtensionCreateViewModel model);

        Task<int> UpdatePaymentExtension(PaymentExtensionUpdateViewModel model);

        Task<int> DeletePaymentExtension(int PeID);

        Task<PagedResult<PaymentExtensionVm>> GetAllPage(RequestPaging request);
    }
}