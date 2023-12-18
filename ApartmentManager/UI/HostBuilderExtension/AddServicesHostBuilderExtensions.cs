using AM.UI.State.Navigators;
using Data.Entity;
using Data.Relationships;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common;
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
                services.AddSingleton<IPaymentExtension, PaymentExtensionServices>();
                services.AddSingleton<IStatistics, StatisticsService>();
                services.AddSingleton<IRentalContract, RentalContractServices>();
                services.AddSingleton<IBill, BillServices>();
                services.AddSingleton<IHome, HomeServices>();
                services.AddSingleton<IStatistics, StatisticsService>();
                services.AddSingleton<IBaseControl<People>, BaseControlServices<People>>();
                services.AddSingleton<IBaseControl<Bill>, BaseControlServices<Bill>>();
                services.AddSingleton<IBaseControl<Room>, BaseControlServices<Room>>();
                services.AddSingleton<IBaseControl<RoomImage>, BaseControlServices<RoomImage>>();
                services.AddSingleton<IBaseControl<Furniture>, BaseControlServices<Furniture>>();
                services.AddSingleton<IBaseControl<DepositsContract>, BaseControlServices<DepositsContract>>();
                services.AddSingleton<IBaseControl<RentalContract>, BaseControlServices<RentalContract>>();
                services.AddSingleton<IBaseControl<Statistics>, BaseControlServices<Statistics>>();

                services.AddSingleton<IStorageService, FileStorageService>();
            });

            return host;
        }
    }
}