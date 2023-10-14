﻿using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel.Dtos;
using ViewModel.People;

namespace Services.Implement
{
    public class PeopleServices : IPeople
    {
        private readonly ApartmentDbContextFactory _contextfactory;
        private readonly IBaseControl<People> _baseControl;

        public PeopleServices(ApartmentDbContextFactory contextfactory, IBaseControl<People> baseControl)
        {
            _contextfactory=contextfactory;
            _baseControl=baseControl;
        }

        public async Task<People> Create(PeopleCreateViewModel request)
        {
            MessageBox.Show(request.ID.ToString());
            People people = new People
            {
                Name = request.Name,
                Sex = request.Sex,
                Birthday= request.Birthday,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IDCard = request.IDCard,
                Address = request.Address,
            };
            var result = await _baseControl.Create(people);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                People entity = await _context.People.FirstOrDefaultAsync((x) => x.ID==id);
                if (entity == null) return false;
                _context.People.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<People> Edit(int id, PeopleUpdateViewModel request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                People people = new People
                {
                    ID = id,
                    Name = request.Name,
                    Sex = request.Sex,
                    Birthday= request.Birthday,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    IDCard = request.IDCard,
                    Address = request.Address,
                };

                EntityEntry<People> result = _context.People.Update(people);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<List<CustomerVM>> GetAll()
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.People
                            join pt in _context.PeopleRental on p.ID equals pt.IDPeople
                            join px in _context.RentalContract on pt.IDRental equals px.ID
                            join pp in _context.Room on px.IDroom equals pp.ID
                            select new { p, pp };
                 var result = await query.Select(e => new CustomerVM
                 {
                     ID = e.p.ID,
                     RoomName = e.pp.Name,
                     Name = e.p.Name,
                     Sex = e.p.Sex,
                     Birthday = e.p.Birthday,
                     PhoneNumber = e.p.PhoneNumber,
                     Email = e.p.Email,
                     IDCard = e.p.IDCard,
                     Address = e.p.Address,
                 }).ToListAsync();
                return result;
            }      
           
        }

        public async Task<PagedResult<CustomerVM>> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                var query = from p in _context.People
                            select p;
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    query = query.Where(x => x.ID.Equals(request.Keyword) || x.Name.Contains(request.Keyword));
                }
                int totalRow = await query.CountAsync();
                var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new CustomerVM()
                    {
                        ID = x.ID,
                        Name = x.Name,
                        Sex = x.Sex,
                        Address = x.Address,
                        Birthday = x.Birthday,
                        Email = x.Email,
                        IDCard =x.IDCard,
                        PhoneNumber = x.PhoneNumber,
                    }).ToListAsync();
                var pagedView = new PagedResult<CustomerVM>()
                {
                    TotalRecords = totalRow,
                    PageIndex = request.PageIndex,
                    PageSize = request.PageSize,
                    Items = data
                };
                return pagedView;
            }
        }

        public async Task<List<CustomerForCombobox>> GetIdNameForCombobox()
        {
            List<People> result;
            using (AparmentDbContext _context = _contextfactory.CreateDbContext())
            {
                result = await _context.People.ToListAsync();
            }
            var result1 = result.Select(e => new CustomerForCombobox
            {
                ID = e.ID,
                Name = e.Name

            }).ToList();
            return result1;
        }
    }
}