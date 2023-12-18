using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.DepositsContract;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Room;

namespace Services.Implement
{
    public class DepositsContractServices : IDepositsContract
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<DepositsContract> _baseControl;

        public DepositsContractServices(ApartmentDbContextFactory contextfactory, IBaseControl<DepositsContract> baseControl)
        {
            _contextfactory = contextfactory;
            _baseControl = baseControl;
        }

        public async Task<DepositsContract> CreateDepositsContract(DepositsContractCreateViewModel model)
        {
            var create = new DepositsContract
            {
                ID = model.ID,
                IDRoom = model.Room.ID,
                IDLeader = model.Customer.ID,
                DepositsDate = model.DepositsDate,
                ReceiveDate = model.ReceiveDate,
                CheckOutDate = model.CheckOutDate,
                Money = model.Money
            };
            return await _baseControl.Create(create);
        }

        public async Task<bool> DeleteDepositsContract(int depositsId)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                DepositsContract deposit = await _context.DepositsContract.FirstOrDefaultAsync(x => x.ID == depositsId);
                if (deposit == null) return false;
                _context.DepositsContract.Remove(deposit);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<PagedResult<DepositsContractVm>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.DepositsContract
                            join pt in _context.Room on p.IDRoom equals pt.ID
                            join px in _context.People on p.IDLeader equals px.ID
                            select new { p, pt, px };
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new DepositsContractVm()
                    {
                        ID = x.p.ID,
                        RoomName = x.pt.Name,
                        LeaderName = x.px.Name,
                        DepositsDate = x.p.DepositsDate,
                        ReceiveDate = x.p.ReceiveDate,
                        CheckOutDate = x.p.CheckOutDate,
                        Money = x.p.Money
                    }).ToListAsync();
                var pagedView = new PagedResult<DepositsContractVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public async Task<DepositsContract> UpdateDepositsContract(DepositsContractUpdateViewModel model)
        {
            var result = new DepositsContract();
            result.ID = model.ID;
            result.IDRoom = model.Room.ID;
            result.DepositsDate = model.DepositsDate;
            result.ReceiveDate = model.ReceiveDate;
            result.CheckOutDate = model.CheckOutDate;
            result.Money = model.Money;
            return await _baseControl.Update(model.ID, result);
        }
    }
}