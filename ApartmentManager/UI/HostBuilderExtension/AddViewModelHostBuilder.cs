using AM.UI.Model;
using AM.UI.State;
using AM.UI.State.Store;
using AM.UI.Utilities;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.DepositContract;
using AM.UI.ViewModelUI.Room;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.HostBuilderExtension
{
    public static class AddViewModelHostBuilder
    {
        public static IHostBuilder AddViewModels(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddTransient<Apartment>();
                services.AddSingleton<ApartmentStore>();
                services.AddSingleton<BillStore>();
                services.AddSingleton<DepositContractStore>();
                services.AddSingleton<FurnitureStore>();
                services.AddSingleton<PaymentExtensionStore>();
                services.AddSingleton<RentalContractStore>();
                services.AddSingleton<RoomStore>();
                services.AddSingleton<HomeStore>();
                services.AddSingleton<ComboboxStore>();
                services.AddSingleton<StatisticsStore>();
                services.AddSingleton<NavigationVM>();
            });

            return hostBuilder;
        }

        /*  private static NavigationVM CreateReservationListingViewModel(IServiceProvider services)
          {
              return NavigationVM.LoadViewModel(services.GetRequiredService<Navigation>(),
                  services.GetRequiredService<NavigationService<HomeVM>>(),
                  services.GetRequiredService<NavigationService<CustomerVMUI>>(),
                  services.GetRequiredService<NavigationService<RoomHomeVMUI>>(),
                  services.GetRequiredService<NavigationService<RoomAddVMUI>>());
          }*/
    }
}