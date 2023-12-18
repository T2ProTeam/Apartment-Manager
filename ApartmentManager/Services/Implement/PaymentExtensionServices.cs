using Azure.Core;
using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.PaymentExtension;
using ViewModel.People;

namespace Services.Implement
{
    public class PaymentExtensionServices : IPaymentExtension
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBill _Ibill;

        public PaymentExtensionServices(ApartmentDbContextFactory contextfactory, IBill ibill)
        {
            _contextfactory = contextfactory;
            _Ibill=ibill;
        }

        public async Task<PaymentExtension> CreatePaymentExtension(PaymentExtensionCreateViewModel model)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                PaymentExtension enity = new PaymentExtension
                {
                    IDBill = model.IDBill,
                    Days = model.Days,
                };

                var value = _context.PaymentExtension.Add(enity);
                await _context.SaveChangesAsync();
                return value.Entity;
            }
        }

        public async Task<int> DeletePaymentExtension(int PeID)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                PaymentExtension entity = await _context.PaymentExtension.FirstOrDefaultAsync(x => x.ID ==PeID);
                int result = 0;
                if (entity != null)
                {
                    var resultupdate = await _Ibill.UpdateActiveBill(entity.IDBill);
                    _context.PaymentExtension.Remove(entity);
                    var resulremove = await _context.SaveChangesAsync();
                    if (resulremove == resultupdate)
                        result=resulremove;
                }
                return result;
            }
        }

        public async Task<List<PaymentExtensionVm>> GetAll()
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.PaymentExtension
                            join pc in _context.Bill on p.IDBill equals pc.ID
                            join pt in _context.RentalContract on pc.IDRTC equals pt.ID
                            join px in _context.Room on pt.IDroom equals px.ID
                            join er in _context.PeopleRental on pt.ID equals er.IDRental
                            join pz in _context.People on er.IDPeople equals pz.ID
                            where er.Membership == Data.Enum.Membership.Leader
                            select new { pc, p, px, pz };
                var data = await
                    query.Select(x => new PaymentExtensionVm()
                    {
                        ID = x.p.ID,
                        IDBill = x.p.IDBill,
                        NameRoom = x.px.Name,
                        NameLeader = x.pz.Name,
                        TotalMoney = x.pc.TotalMoney,
                        Days = x.p.Days
                    }).ToListAsync();
                return data;
            }
        }

        public async Task<List<PaymentExtensionVm>> GetAllDate(DateTime date)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.PaymentExtension
                            join pc in _context.Bill on p.IDBill equals pc.ID
                            join pt in _context.RentalContract on pc.IDRTC equals pt.ID
                            join px in _context.Room on pt.IDroom equals px.ID
                            join er in _context.PeopleRental on pt.ID equals er.IDRental
                            join pz in _context.People on er.IDPeople equals pz.ID
                            where p.Days.Month == date.Month && p.Days.Year == date.Year && er.Membership == Data.Enum.Membership.Leader
                            select new { pc, p, px, pz };
                var data = await
                    query.Select(x => new PaymentExtensionVm()
                    {
                        ID = x.p.ID,
                        IDBill = x.p.IDBill,
                        NameRoom = x.px.Name,
                        NameLeader = x.pz.Name,
                        TotalMoney = x.pc.TotalMoney,
                        Days = x.p.Days
                    }).ToListAsync();
                return data;
            }
        }

        public async Task<PagedResult<PaymentExtensionVm>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.PaymentExtension
                            join pt in _context.Bill on p.IDBill equals pt.ID
                            select new { p, pt };
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new PaymentExtensionVm()
                    {
                        ID = x.p.ID,
                        IDBill = x.pt.ID,
                        Days = x.p.Days
                    }).ToListAsync();
                var pagedView = new PagedResult<PaymentExtensionVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public Task<int> UpdatePaymentExtension(PaymentExtensionUpdateViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}