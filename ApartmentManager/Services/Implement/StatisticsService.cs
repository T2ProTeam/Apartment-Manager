using Azure.Core;
using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.XPath;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Statistic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services.Implement
{
    public class StatisticsService : IStatistics
    {
        private readonly ApartmentDbContextFactory _ApartmentFactory;
        private readonly IBaseControl<Statistics> _BaseControl;

        public StatisticsService(ApartmentDbContextFactory apartmentFactory, IBaseControl<Statistics> BaseControl)
        {
            _ApartmentFactory = apartmentFactory;
            _BaseControl = BaseControl;
        }

        public async Task<Statistics> CreateGovermentMoney(int Month, int Year)
        {
            using (AparmentDbContext _context = _ApartmentFactory.CreateDbContext())
            {
                var finding = await _context.Statistics.FirstOrDefaultAsync(x => x.Month.Equals(Month) && x.Year.Equals(Year));
                if (finding == null)
                {
                    var Statistics = new Statistics
                    {
                        ElectricMoneyOfGovernment = 0,
                        WaterMoneyOfGovernment = 0,
                        ServiceOfGovernment = 0,
                        Month = Month,
                        Year = Year
                    };
                    return await _BaseControl.Create(Statistics);
                }
                return null;
            }
        }

        public async Task<MoneyOfGovernment> GetGovermentMoney(int Month, int Year)
        {
            using (AparmentDbContext _context = _ApartmentFactory.CreateDbContext())
            {
                var query = _context.Statistics.FirstOrDefault(x => x.Month.Equals(Month) && x.Year.Equals(Year));

                if (query == null)
                {
                    var NullResult = new MoneyOfGovernment
                    {
                        ID = 0,
                        ElectricMoneyOfGovernment = 0,
                        WaterMoneyOfGovernment = 0,
                        ServiceOfGovernment = 0,
                    };
                    return NullResult;
                }
                var result = new MoneyOfGovernment
                {
                    ID = query.ID,
                    ElectricMoneyOfGovernment = query.ElectricMoneyOfGovernment,
                    WaterMoneyOfGovernment = query.WaterMoneyOfGovernment,
                    ServiceOfGovernment = query.ServiceOfGovernment,
                };

                return result;
            }
        }

        public async Task<PagedResult<StatisticsVm>> GetAllPage(RequestPaging request, int Month, int Year)
        {
            using (AparmentDbContext _context = _ApartmentFactory.CreateDbContext())
            {
                var query = from p in _context.Bill
                            join pt in _context.RentalContract on p.IDRTC equals pt.ID
                            join px in _context.Room on pt.IDroom equals px.ID
                            join pe in _context.PeopleRental on pt.ID equals pe.IDRental
                            join pp in _context.People on pe.IDPeople equals pp.ID
                            where p.PayDate.Month == Month && p.PayDate.Year == Year
                            select new { p, pt, px, pp };
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new StatisticsVm()
                    {
                        RoomName = x.px.Name,
                        LeaderName = x.pp.Name,
                        TotalMoney = (x.p.ElectricQuantity * x.pt.ElectricMoney) + (x.pt.WaterMoney * x.px.Quantity) + (x.pt.ServiceMoney * x.px.Quantity),
                        DatePaid = x.p.PayDate,
                    }).ToListAsync();
                var pagedView = new PagedResult<StatisticsVm>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public async Task<StatisticTotalMoney> GetElectricAndWater(int Month, int Year)
        {
            using (AparmentDbContext _context = _ApartmentFactory.CreateDbContext())
            {
                var query = from p in _context.RentalContract
                            join pt in _context.Bill on p.ID equals pt.IDRTC
                            join px in _context.Room on p.IDroom equals px.ID
                            where pt.PayDate.Month == Month && pt.PayDate.Year == Year
                            select new { p, pt, px };

                var result = query.Select(x => new StatisticTotalMoney
                {
                    ElectricMoney = x.p.ElectricMoney * x.pt.ElectricQuantity,
                    WaterMoney = x.p.WaterMoney * x.px.Quantity,
                    ServiceMoney = x.p.ServiceMoney * x.px.Quantity,
                }).ToList();

                var total = new StatisticTotalMoney
                {
                    ElectricMoney = result.Sum(x => x.ElectricMoney),
                    WaterMoney = result.Sum(x => x.WaterMoney),
                    ServiceMoney = result.Sum(x => x.ServiceMoney)
                };

                return total;
            }
        }

        public async Task<Statistics> EditGovernmentMoney(EditGorvernmentMoney edit)
        {
            using (AparmentDbContext _context = _ApartmentFactory.CreateDbContext())
            {
                var statistics = await _context.Statistics.FindAsync(edit.ID);

                statistics.ElectricMoneyOfGovernment = edit.ElectricMoneyOfGovernment;
                statistics.WaterMoneyOfGovernment = edit.WaterMoneyOfGovernment;
                statistics.ServiceOfGovernment = edit.ServiceOfGovernment;
                statistics.Month = edit.Month;
                statistics.Year = edit.Year;

                var result = _context.Statistics.Update(statistics);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<StatisticsProfitVm> GetProfitMoney(int Month, int Year)
        {
            var total = await GetElectricAndWater(Month, Year);
            var government = await GetGovermentMoney(Month, Year);
            StatisticsProfitVm profit = new StatisticsProfitVm
            {
                ElectricMoney = total.ElectricMoney - government.ElectricMoneyOfGovernment,
                WaterMoney = total.WaterMoney - government.WaterMoneyOfGovernment,
                ServiceMoney = total.ServiceMoney - government.ServiceOfGovernment,
            };
            return profit;
        }
    }
}