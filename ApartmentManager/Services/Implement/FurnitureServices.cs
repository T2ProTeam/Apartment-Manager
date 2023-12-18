using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;

namespace Services.Implement
{
    public class FurnitureServices : IFurniture
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<Furniture> _base;

        public FurnitureServices(ApartmentDbContextFactory contextfactory, IBaseControl<Furniture> basee)
        {
            _contextfactory = contextfactory;
            _base = basee;
        }

        public async Task<Furniture> CreateFurniture(FurnitureCreateViewModel request)
        {
            var fur = new Data.Entity.Furniture()
            {
                ID = request.ID,
                Name = request.Name,
            };
            var result = await _base.Create(fur);
            return result;
        }

        public async Task<bool> DeleteFurniture(int FurnitureId)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                Furniture entity = await _context.Furniture.FirstOrDefaultAsync((x) => x.ID == FurnitureId);
                if (entity == null) return false;
                _context.Furniture.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<FurnitureVm>> GetAll()
        {
            List<Furniture> result = await _base.GetAll();
            var FurResult = result.Select(x => new FurnitureVm()
            {
                ID = x.ID,
                Name = x.Name
            }).ToList();
            return FurResult;
        }

        public async Task<PagedResult<FurnitureVm>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Furniture
                            select p;
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new FurnitureVm()
                    {
                        ID = x.ID,
                        Name = x.Name,
                    }).ToListAsync();
                var pagedView = new PagedResult<FurnitureVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        


        public async Task<Furniture> UpdateFurniture(FurnitureUpdateViewModel model)
        {
            var furniture = new Furniture();
            furniture.ID = model.ID;
            furniture.Name = model.Name;
            return await _base.Update(furniture.ID, furniture);
        }
    }
}