using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.RentalContract;

namespace Services.Implement
{
    public class RentalContractServices : IRentalContract
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<RentalContract> _base;

        public RentalContractServices(ApartmentDbContextFactory contextfactory, IBaseControl<RentalContract> baseControl)
        {
            _contextfactory = contextfactory;
            _base = baseControl;
        }

        public async Task<RentalContract> Create(RentalContractCreateViewModel model)
        {
            var rental = new Data.Entity.RentalContract
            {
                ID = model.ID,
                IDroom = model.RoomCombobox.ID,
                ReceiveDate = model.ReceiveDate,
                CheckOutDate = model.CheckOutDate,
                RoomMoney = model.RoomMoney,
                ElectricMoney = model.ElectricMoney,
                WaterMoney = model.WaterMoney,
                ServiceMoney = model.ServiceMoney,
                Active = model.Active,
            };
            return await _base.Create(rental);
        }

        public async Task<bool> Delete(int id)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var result = _context.RentalContract.FirstOrDefault(x => x.ID == id);
                if (result == null) return false;
                _context.RentalContract.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public Task<List<RentalContractVm>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<RentalContractVm>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var rental = from p in _context.RentalContract
                             join pt in _context.Room on p.IDroom equals pt.ID
                             select new { p, pt };
                var rs = await rental.Select(x => new RentalContractVm
                {
                    ID = x.p.ID,
                    RoomName = x.pt.Name,
                    LeaderName = "None",
                    ReceiveDate = x.p.ReceiveDate,
                    CheckOutDate = x.p.CheckOutDate,
                    RoomMoney = x.p.RoomMoney,
                    ElectricMoney = x.p.ElectricMoney,
                    WaterMoney = x.p.WaterMoney,
                    ServiceMoney = x.p.ServiceMoney,
                    Active = x.p.Active
                }).ToListAsync();

                var query = from p in _context.RentalContract
                            join pt in _context.Room on p.IDroom equals pt.ID
                            join px in _context.PeopleRental on p.ID equals px.IDRental
                            join pp in _context.People on px.IDPeople equals pp.ID
                            where px.Membership == Data.Enum.Membership.Leader
                            select new { p, pt, pp };
                rs.ForEach(x =>
                {
                    foreach (var a in query)
                    {
                        if (x.ID == a.p.ID)
                        {
                            x.LeaderName = a.pp.Name;
                        }
                    }
                });

                int totalRow = await query.CountAsync();

                var pagedView = new PagedResult<RentalContractVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = rs
                };
                return pagedView;
            }
        }

        public async Task<List<RentalContractForCombobox>> GetAllRental()
        {
            List<RentalContract> result;
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                result = await _context.RentalContract.ToListAsync();
            }
            var result1 = result.Select(e => new RentalContractForCombobox
            {
                IDRental = e.ID,
                ElectricMoney = e.ElectricMoney,
                WaterMoney = e.WaterMoney,
                ServiceMoney = e.ServiceMoney
            }).ToList();
            return result1;
        }

        public Task<RentalContract> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<RentalContract> Update(RentalContractUpdateViewModel model)
        {
            var update = new Data.Entity.RentalContract();
            update.ID = model.ID;
            update.IDroom = model.RoomCombobox.ID;
            update.ReceiveDate = model.ReceiveDate;
            update.CheckOutDate = model.CheckOutDate;
            update.RoomMoney = model.RoomMoney;
            update.ElectricMoney = model.ElectricMoney;
            update.WaterMoney = model.WaterMoney;
            update.ServiceMoney = model.ServiceMoney;
            update.Active = model.Active;
            return await _base.Update(model.ID, update);
        }
    }
}