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
using System.Windows;
using ViewModel.Bill;
using ViewModel.Dtos;
using ViewModel.People;
using ViewModel.Room;

namespace Services.Implement
{
    public class BillServices : IBill
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<Bill> _baseControl;
        private readonly IRoom _Iroom;

        public BillServices(ApartmentDbContextFactory contextfactory, IBaseControl<Bill> baseControl, IRoom iroom)
        {
            _contextfactory = contextfactory;
            _baseControl = baseControl;
            _Iroom = iroom;
        }

        public async Task<Bill> CreateBill(BillCreateViewModel model)
        {
            int Quantity = await _Iroom.GetQuantity();
            var bill = new Bill
            {
                ID = model.ID,
                IDRTC = model.Rental.IDRental,
                ElectricQuantity = model.ElectricQuantity,
                Active = model.Active,
                TotalMoney = (model.Rental.ElectricMoney * model.ElectricQuantity) + (model.Rental.WaterMoney * Quantity) + (model.Rental.ServiceMoney * Quantity),
                PayDate = model.PayDate
            };
            MessageBox.Show(bill.TotalMoney.ToString());
            return await _baseControl.Create(bill);
        }

        public async Task<bool> DeleteBill(int ID)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var result = await _context.Bill.FirstOrDefaultAsync(x => x.ID == ID);
                if (result == null) return false;
                _context.Bill.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<PagedResult<BillVm>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Bill
                            join pt in _context.RentalContract on p.IDRTC equals pt.ID
                            join pp in _context.Room on pt.IDroom equals pp.ID
                            select new { p, pt, pp };
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new BillVm()
                    {
                        ID = x.p.ID,
                        IDRTC = x.pt.ID,
                        ElectricQuantity = x.p.ElectricQuantity,
                        Active = x.p.Active,
                        PayDate = x.p.PayDate,
                        TotalMoney = (x.p.ElectricQuantity * x.pt.ElectricMoney) + (x.pt.WaterMoney * x.pp.Quantity) + (x.pt.ServiceMoney * x.pp.Quantity)
                    }).ToListAsync();
                var pagedView = new PagedResult<BillVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public async Task<int> UpdateActiveBill(int model)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                Bill _bill = await _context.Bill.FirstOrDefaultAsync(x => x.ID == model);
                _bill.Active = Data.Enum.Active.Yes;
                _context.Bill.Update(_bill);
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<Bill> UpdateBill(BillUpdateViewModel model)
        {
            var bill = new Bill();
            bill.ID = model.ID;
            bill.IDRTC = model.Rental.IDRental;
            bill.ElectricQuantity = model.ElectricQuantity;
            bill.Active = model.Active;
            bill.PayDate = model.PayDate;
            bill.TotalMoney = model.TotalMoney;
            return await _baseControl.Update(model.ID, bill);
        }
    }
}