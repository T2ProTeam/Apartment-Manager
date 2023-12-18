using Data.Entity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Bill;
using ViewModel.DepositsContract;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.PaymentExtension;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;
using ViewModel.RoomDetails;
using ViewModel.Statistic;

namespace AM.UI.Model
{
    public class Apartment
    {
        private readonly IPeople _Ipeople;
        private readonly IRoom _Iroom;
        private readonly IRoomDetails _iroomdetail;
        private readonly IBill _Ibill;
        private readonly IDepositsContract _IdepositContract;
        private readonly IFurniture _Ifurniture;
        private readonly IPaymentExtension _IpaymentExtension;
        private readonly IRentalContract _IrentalContract;
        private readonly IStatistics _IStatistics;

        public Apartment(IPeople Ipeople, IRoom iroom, IRoomDetails iroomdetail, IDepositsContract idepositContract, IBill ibill,
                        IFurniture ifurniture, IPaymentExtension ipaymentExtension, IRentalContract irentalContract, IStatistics IStatistics)
        {
            _Ipeople = Ipeople;
            _Iroom = iroom;
            _iroomdetail = iroomdetail;
            _IdepositContract = idepositContract;
            _Ifurniture = ifurniture;
            _Ibill = ibill;
            _IpaymentExtension = ipaymentExtension;
            _IrentalContract = irentalContract;
            _IStatistics = IStatistics;
        }

        //RentalContract
        public async Task<List<RentalContractVm>> GetAllRentalContract()
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 40 };
            PagedResult<RentalContractVm> rental = await _IrentalContract.GetAllPage(paged);
            List<RentalContractVm> data = rental.Items;
            return data;
        }

        //Furniture
        public async Task<List<FurnitureVm>> GetAllFurniture()
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 40 };
            PagedResult<FurnitureVm> furniture = await _Ifurniture.GetAllPage(paged);
            List<FurnitureVm> data = furniture.Items;
            return data;
        }

        //PaymentExtension

        public async Task<List<PaymentExtensionVm>> GetAllPaymentExtension()
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 40 };
            PagedResult<PaymentExtensionVm> payment = await _IpaymentExtension.GetAllPage(paged);
            List<PaymentExtensionVm> data = payment.Items;
            return data;
        }

        //DepositContract
        public async Task<List<DepositsContractVm>> GetAllDepositContract()
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 40 };
            PagedResult<DepositsContractVm> deposit = await _IdepositContract.GetAllPage(paged);
            List<DepositsContractVm> data = deposit.Items;
            return data;
        }

        //Bill
        public async Task<List<BillVm>> GetAllBill()
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 40 };
            PagedResult<BillVm> bill = await _Ibill.GetAllPage(paged);
            List<BillVm> data = bill.Items;
            return data;
        }

        //Room Details
        public async Task<List<RoomDetailsFurniture>> GetAllRoomDetailsFurniture(int id)
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 40 };
            PagedResult<RoomDetailsFurniture> roomdetails = await _iroomdetail.GetAllFurniture(paged, id);
            List<RoomDetailsFurniture> data = roomdetails.Items;
            return data;
        }

        public async Task<List<RoomDetailsImage>> GetAllRoomDetailsImage(int id)
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 40 };
            PagedResult<RoomDetailsImage> roomdetailsImage = await _iroomdetail.GetAllImage(paged, id);
            List<RoomDetailsImage> data = roomdetailsImage.Items;
            return data;
        }

        // Room
        public async Task<List<RoomVm>> GettAllRoom()
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 100 };
            PagedResult<RoomVm> room = await _Iroom.GetAllPage(paged);
            List<RoomVm> data = room.Items;
            return data;
        }

        //Customer
        public async Task<List<CustomerVM>> GetAllcustomer()
        {
            RequestPaging a = new RequestPaging();
            a.Keyword = null;
            a.PageSize = 100;
            a.PageIndex = 1;
            PagedResult<CustomerVM> tes = await _Ipeople.GetAllPage(a);
            List<CustomerVM> data = tes.Items;
            return data;
        }

        //Statistics

        public async Task<List<StatisticsVm>> GetAllStaticstis(int Month, int Year)
        {
            RequestPaging paged = new RequestPaging { Keyword = null, PageIndex = 1, PageSize = 100 };
            PagedResult<StatisticsVm> statistics = await _IStatistics.GetAllPage(paged, Month, Year);
            List<StatisticsVm> data = statistics.Items;
            return data;
        }
    }
}