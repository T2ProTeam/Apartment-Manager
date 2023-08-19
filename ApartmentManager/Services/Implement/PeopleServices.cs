﻿using Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
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
            People people = new People
            {
                IDroom = request.IDroom,
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
            return await _baseControl.Delete(id);
        }

        public async Task<People> Edit(int id, PeopleUpdateViewModel request)
        {
            People people = new People
            {
                IDroom = request.IDroom,
                Name = request.Name,
                Sex = request.Sex,
                Birthday= request.Birthday,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                IDCard = request.IDCard,
                Address = request.Address,
            };
            var result = await _baseControl.Update(id, people);
            return result;
        }

        public async Task<List<CustomerVM>> GetAll()
        {
            List<People> result = await _baseControl.GetAll();
            var result1 = result.Select(e => new CustomerVM
            {
                ID = e.ID,
                IDroom = e.IDroom,
                Name = e.Name,
                Sex = e.Sex,
                Birthday= e.Birthday,
                PhoneNumber = e.PhoneNumber,
                Email = e.Email,
                IDCard = e.IDCard,
                Address = e.Address,
            }).ToList();
            return result1;
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
                        IDroom = x.IDroom,
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
    }
}