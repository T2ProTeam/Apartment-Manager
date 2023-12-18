using Data;
using Data.Entity;
using Data.Enum;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Home;

namespace Services.Implement
{
    public class HomeServices : IHome
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<Bill> _Ibill;

        public HomeServices(ApartmentDbContextFactory contextfactory, IBaseControl<Bill> Ibill)
        {
            _contextfactory = contextfactory;
            _Ibill = Ibill;
        }

        public async Task<ResultAutoAddVM> AutoAdd(AutoAddHomeVM request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                ResultAutoAddVM temp = new ResultAutoAddVM();
                var listrc = await _context.RentalContract.ToListAsync();
                var listbill = from p in _context.Bill
                               where p.PayDate.Month == request.date.Month && p.PayDate.Year == request.date.Year
                               select p;
                if (!listbill.Any())
                {
                    int result = 0;
                    foreach (var item in listrc)
                    {
                        Bill bill = new Bill
                        {
                            IDRTC = item.ID,
                            ElectricQuantity =0,
                            Active = Active.No,
                            PayDate = request.date.AddDays(5),
                            TotalMoney = 0,
                        };

                        var result1 = await _Ibill.Create(bill);
                        if (result1 != null)
                        {
                            result =1;
                        }
                        else result = 0;
                    }
                    await _context.SaveChangesAsync();
                    var listbilladd = await GetDataBase(request.date);
                    temp.result= result;
                    temp.items = listbilladd;
                    return temp;
                }
                temp.result =-1;
                temp.items=null;
                return temp;
            }
        }

        public async Task<List<HomeItemVM>> GetDataBase(DateTime date)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Bill
                            join pt in _context.RentalContract on p.IDRTC equals pt.ID
                            join px in _context.Room on pt.IDroom equals px.ID
                            where p.PayDate.Month == date.Month && p.PayDate.Year == date.Year
                            select new { p, pt, px };

                var payment = from p in _context.PaymentExtension
                              join pt in _context.Bill on p.IDBill equals pt.ID
                              where pt.PayDate.Month == date.Month &&    pt.PayDate.Year == date.Year
                              select p;
                if (payment.Any())
                {
                    query = query.Where(bill => !payment.Any(payment => payment.IDBill == bill.p.ID));
                }
                var result = await query.Select(x => new HomeItemVM
                {
                    ID = x.p.ID,
                    NameRoom = x.px.Name,
                    ElecQuality = x.p.ElectricQuantity,
                    RoomMoney = x.pt.RoomMoney,
                    ElectricMoney = x.pt.ElectricMoney,
                    WaterMoney = x.pt.WaterMoney,
                    ServiceMoney = x.pt.ServiceMoney,
                    PayDate = x.p.PayDate,
                    TotalMoney =x.p.TotalMoney,
                    Active = x.p.Active,
                    IsActive = Convert.ToBoolean(x.p.Active)
                }).ToListAsync();
                return result;
            }
        }

        public async Task<HomeItemVM> GetByIDBill(int ID, DateTime date)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.Bill
                            join pt in _context.RentalContract on p.IDRTC equals pt.ID
                            join px in _context.Room on pt.IDroom equals px.ID
                            where p.PayDate.Month == date.Month && p.PayDate.Year == date.Year
                            select new { p, pt, px };
                var payment = from p in _context.PaymentExtension
                              join pt in _context.Bill on p.IDBill equals pt.ID
                              where pt.PayDate.Month == date.Month &&    pt.PayDate.Year == date.Year
                              select p;
                if (payment.Any())
                {
                    query = query.Where(bill => !payment.Any(payment => payment.IDBill == bill.p.ID));
                }
                var result = await query.Select(x => new HomeItemVM
                {
                    ID = x.p.ID,
                    NameRoom = x.px.Name,
                    ElecQuality = x.p.ElectricQuantity,
                    RoomMoney = x.pt.RoomMoney,
                    ElectricMoney = x.pt.ElectricMoney,
                    WaterMoney = x.pt.WaterMoney,
                    ServiceMoney = x.pt.ServiceMoney,
                    PayDate = x.p.PayDate,
                    TotalMoney =x.p.TotalMoney,
                    Active = x.p.Active,
                    IsActive = Convert.ToBoolean(x.p.Active)
                }).FirstOrDefaultAsync(x => x.ID == ID);

                return result;
            }
        }

        public async Task<NumberOfHomeVM> GetNumberOfHomeVM(DateTime date)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var querycus = await _context.People.ToListAsync();
                var queryroom = from p in _context.Room
                                join pt in _context.RentalContract on p.ID equals pt.IDroom into grouping
                                from px in grouping.DefaultIfEmpty()
                                where px == null
                                select p;

                var queryBill = from p in _context.Bill
                                join pt in _context.PaymentExtension on p.ID equals pt.IDBill
                                where p.PayDate.Month == date.Month && p.PayDate.Year == date.Year
                                select new { p, pt };

                NumberOfHomeVM Object = new NumberOfHomeVM
                {
                    NumberCustomer= querycus.Count(),
                    NumberRoom =  await queryroom.CountAsync(),
                    NumberPE = await queryBill.CountAsync(),
                };
                return Object;
            }
        }

        public async Task<Bill> updateElectric(UpdateElectricQuanlity request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var Model = await _context.Bill.FirstOrDefaultAsync(x => x.ID ==request.Id);
                Bill bill = new Bill
                {
                    ID = request.Id,
                    ElectricQuantity = request.Quality,
                    IDRTC = Model.IDRTC,
                    PayDate = Model.PayDate,
                    Active = Model.Active,
                    TotalMoney = request.TotalMoney
                };
                Bill result = await _Ibill.Update(bill.ID, bill);
                return result;
            }
        }

        public async Task<Bill> updateActive(HomeItemVM request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                Active re = (Active)Convert.ToInt32(request.IsActive);
                var listrc = from pt in _context.RentalContract
                             join px in _context.Room on pt.IDroom equals px.ID
                             where px.Name == request.NameRoom
                             select new { px, pt };
                var asd = await listrc.FirstOrDefaultAsync();
                Bill bill = new Bill
                {
                    ID= request.ID,
                    IDRTC= asd.pt.ID,
                    ElectricQuantity= request.ElecQuality,
                    PayDate = request.PayDate,
                    TotalMoney = request.TotalMoney,
                    Active = re,
                };
                var result = await _Ibill.Update(bill.ID, bill);
                return result;
            }
        }
    }
}