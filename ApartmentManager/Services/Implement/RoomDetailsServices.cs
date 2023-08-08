﻿using Data;
using Microsoft.EntityFrameworkCore.Internal;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Dtos;
using ViewModel.Furniture;
using ViewModel.People;
using ViewModel.Room;
using ViewModel.RoomDetails;

namespace Services.Implement
{
    public class RoomDetailsServices : IRoomDetails
    {
        private readonly ApartmentDbContextFactory _contextFactory;
        public RoomDetailsServices(ApartmentDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public PagedResult<RoomDetailsVm> GetAllPage(RequestPaging request)
        {
            using (AparmentDbContext _context = _contextFactory.CreateDbContext())
            {
                var query = from p in _context.RoomDetail
                            join pt in _context.Furniture on p.IDFur equals pt.ID
                            join px in _context.Room on p.IDRoom equals px.ID
                            select new {p,pt,px};
                int totalRow = query.Count();
                var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(x => new RoomDetailsVm()
                    {
                        FurName = x.pt.Name,
                        RoomName = x.px.Name
                    }).ToList();
                var pagedView = new PagedResult<RoomDetailsVm>()
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