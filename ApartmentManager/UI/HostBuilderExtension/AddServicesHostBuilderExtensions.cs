﻿using Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Implement;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.HostBuilderExtension
{
    public static class AddServicesHostBuilderExtensions
    {
        public static IHostBuilder AddServices(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddSingleton<IPeople, PeopleServices>();
                services.AddSingleton<IRoom, RoomServices>();
                services.AddSingleton<IFurniture, FurnitureServices>();
                services.AddSingleton<IRoomDetails, RoomDetailsServices>();
                services.AddSingleton<IRentalContract, RentalContractServices>();
                services.AddSingleton<IBill, BillServices>();
                services.AddSingleton<IDepositsContract, DepositsContractServices>();
            });

            return host;
        }
    }
}