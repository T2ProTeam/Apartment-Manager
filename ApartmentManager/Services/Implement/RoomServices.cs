using Data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Room;

namespace Services.Implement
{
    public class RoomServices : IRoom
    {
        private readonly ApartmentDbContextFactory _contextfactory = new ApartmentDbContextFactory();
        public Task<int> CreateRoom(RoomCreateViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRoom(int ImageId)
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<RoomVm>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Room
                            join pt in _context.People on p.IDLeader equals pt.ID
                            select new { p, pt };
                int totalRow = await  query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new RoomVm()
                    {
                        ID = x.p.ID,
                        NameLeader = x.pt.Name,
                        Name = x.p.Name,
                        Quantity = x.p.Quantity
                    }).ToListAsync();
                var pagedView = new PagedResult<RoomVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public Task<int> UpdateRoom(RoomUpdateViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
