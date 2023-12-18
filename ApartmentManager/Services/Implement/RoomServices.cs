using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Abstractions;
using Services.Interface;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Bill;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Room;

namespace Services.Implement
{
    public class RoomServices : IRoom
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<Room> _base;

        public RoomServices(ApartmentDbContextFactory contextfactory, IBaseControl<Room> baseControl)
        {
            _contextfactory = contextfactory;
            _base = baseControl;
        }

        public async Task<Room> Create(RoomCreateViewModel model)
        {
            var room = new Data.Entity.Room()
            {
                Name = model.Name,
                Quantity = model.Quantity,
                Staked = model.Staked
            };

            var result = await _base.Create(room);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                Room entity = await _context.Room.FirstOrDefaultAsync((x) => x.ID == id);
                if (entity == null) return false;
                _context.Room.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<RoomVm>> GetAll()
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Room
                            join pp in _context.RentalContract on p.ID equals pp.IDroom
                            join px in _context.PeopleRental on pp.ID equals px.IDRental
                            join pt in _context.People on px.IDPeople equals pt.ID
                            select new { p, pt };
                var data = await query.Select(x => new RoomVm()
                {
                    ID = x.p.ID,
                    NameLeader = x.pt.Name,
                    Name = x.p.Name,
                    Staked = x.p.Staked,
                    Quantity = x.p.Quantity
                }).ToListAsync();
                return data;
            }
        }

        public async Task<List<RoomVm>> GetAllEmptyRom()
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Room
                            join pt in _context.RentalContract on p.ID equals pt.IDroom into grouping
                            from px in grouping.DefaultIfEmpty()
                            where px == null
                            select p;

                var data = await query.Select(x => new RoomVm()
                {
                    ID = x.ID,
                    Name = x.Name,
                    Quantity = x.Quantity
                }).ToListAsync();
                return data;
            }
        }

        public async Task<PagedResult<RoomVm>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Room
                            join pp in _context.RentalContract on p.ID equals pp.IDroom
                            join px in _context.PeopleRental on pp.ID equals px.IDRental
                            join pt in _context.People on px.IDPeople equals pt.ID
                            join pd in _context.DepositsContract on p.ID equals pd.IDRoom
                            where px.Membership == Data.Enum.Membership.Leader && pp.Active == Data.Enum.Active.Yes
                            select new { p, pt, px, pp, pd };
                var query1 = await _context.Room.ToListAsync();
                var data1 = query1.Select(x => new RoomVm()
                {
                    ID = x.ID,
                    NameLeader = "None",
                    Name = x.Name,
                    Quantity = x.Quantity,
                    Staked = 0
                }).ToList();
                int totalRow = await query.CountAsync();
                var data = await query.Select(x => new RoomVm()
                {
                    ID = x.p.ID,
                    NameLeader = x.pt.Name,
                    Name = x.p.Name,
                    Quantity = x.p.Quantity,
                    Staked = x.pd.Money
                }).ToListAsync();
                foreach (var result in data1)
                {
                    foreach (var result1 in data)
                    {
                        if (result.ID == result1.ID)
                        {
                            result.NameLeader = result1.NameLeader;
                            result.Staked = result1.Staked;
                        }
                    }
                }

                var pagedView = new PagedResult<RoomVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data1
                };
                return pagedView;
            }
        }

        public Task<PagedResult<RoomVm>> GetAllPageDetails(RequestPaging request)
        {
            throw new NotImplementedException();
        }

        public async Task<Room> GetById(int id)
        {
            return await _base.GetById(id);
        }

        public async Task<List<RoomForCombobox>> GetIdNameRoom()
        {
            List<Room> result;
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                result = await _context.Room.ToListAsync();
            }
            var result1 = result.Select(e => new RoomForCombobox
            {
                ID = e.ID,
                Name = e.Name
            }).ToList();
            return result1;
        }

        public async Task<int> GetQuantity()
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Bill
                            join pt in _context.RentalContract on p.IDRTC equals pt.ID
                            join px in _context.Room on pt.IDroom equals px.ID
                            select px.Quantity;
                return await query.FirstOrDefaultAsync();
            }
        }

        public async Task<Room> Update(RoomUpdateViewModel model)
        {
            var room = new Data.Entity.Room();
            room.ID = model.ID;
            room.Name = model.Name;
            room.Quantity = model.Quantity;
            return await _base.Update(model.ID, room);
        }
    }
}