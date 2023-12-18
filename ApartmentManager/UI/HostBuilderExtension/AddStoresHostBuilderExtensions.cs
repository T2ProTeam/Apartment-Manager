using AM.UI.State.Navigators;
using AM.UI.Utilities;
using AM.UI.View.RoomDetails;
using AM.UI.ViewModelUI;
using AM.UI.ViewModelUI.Bills;
using AM.UI.ViewModelUI.Customer;
using AM.UI.ViewModelUI.DepositContract;
using AM.UI.ViewModelUI.Factory;
using AM.UI.ViewModelUI.Furnitures;
using AM.UI.ViewModelUI.RentalContract;
using AM.UI.ViewModelUI.Room;
using AM.UI.ViewModelUI.RoomDetails;
using AM.UI.ViewModelUI.Statistics;
using Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Bill;
using ViewModel.DepositsContract;
using ViewModel.Furniture;
using ViewModel.PaymentExtension;
using ViewModel.People;
using ViewModel.RentalContract;
using ViewModel.Room;
using ViewModel.RoomDetails;

namespace AM.UI.HostBuilderExtension
{
    public static class AddStoresHostBuilderExtensions
    {
        public static IHostBuilder AddStores(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient<HomeVM>();
                services.AddTransient<CustomerForCombobox>();
                services.AddTransient<RentalContractForCombobox>();
                services.AddTransient<RoomForCombobox>();
                services.AddTransient<CustomerVMUI>();
                services.AddTransient<RoomHomeVMUI>();
                services.AddTransient<RoomAddVMUI>();
                services.AddTransient<RoomUpdateVMUI>();
                services.AddTransient<RoomDeleteVMUI>();
                services.AddTransient<CustomerVM>();
                services.AddTransient<RoomVm>();
                services.AddTransient<RentalContractVm>();
                services.AddTransient<BillVm>();
                services.AddTransient<DepositsContractVm>();
                services.AddTransient<FurnitureVm>();
                services.AddTransient<RoomDetailsImage>();
                services.AddTransient<PaymentExtensionVm>();
                services.AddTransient<BillVm>();
                services.AddTransient<RoomDetailsHomeVMUI>();
                services.AddTransient<FurnitureHomeVMUI>();
                services.AddTransient<RentalContractHomeVMUI>();
                services.AddTransient<BillHomeVMUI>();
                services.AddTransient<BillAddVMUI>();
                services.AddTransient<BillUpdateVMUI>();
                services.AddTransient<DepositContractHomeVMUI>();
                services.AddTransient<AddCustomerVMUI>();
                services.AddTransient<RoomDetailsAddImageVMUI>();
                services.AddTransient<RoomDetailsEnlarge>();
                services.AddTransient<FurnitureAddVMUI>();
                services.AddTransient<FurnitureUpdateVMUI>();
                services.AddTransient<RentalContractAddVMUI>();
                services.AddTransient<RentalContractUpdateVMUI>();
                services.AddTransient<AddCustomerInRentalVMUI>();
                services.AddTransient<DepositContractAddVMUI>();
                services.AddTransient<DepositContractUpdateVMUI>();
                services.AddTransient<RoomDetailsInformationCustomerVMUI>();
                services.AddTransient<PaymentExtensionHomeVMUI>();
                services.AddTransient<StatisticsHomeVMUI>();
                /*-------------------------------------------------------------------------------*/
                services.AddSingleton<INavigator, Navigator>();
                services.AddSingleton<IAparmentViewModelFactory, AparmentViewModelFactory>();
                services.AddSingleton<CreateViewModel<HomeVM>>(services => () => services.GetRequiredService<HomeVM>());
                services.AddSingleton<CreateViewModel<CustomerVMUI>>(services => () => services.GetRequiredService<CustomerVMUI>());
                services.AddSingleton<CreateViewModel<RoomHomeVMUI>>(services => () => services.GetRequiredService<RoomHomeVMUI>());
                services.AddSingleton<CreateViewModel<RoomAddVMUI>>(services => () => services.GetRequiredService<RoomAddVMUI>());
                services.AddSingleton<CreateViewModel<RoomUpdateVMUI>>(services => () => services.GetRequiredService<RoomUpdateVMUI>());
                services.AddSingleton<CreateViewModel<RoomDetailsHomeVMUI>>(services => () => services.GetRequiredService<RoomDetailsHomeVMUI>());
                services.AddSingleton<CreateViewModel<FurnitureHomeVMUI>>(services => () => services.GetRequiredService<FurnitureHomeVMUI>());
                services.AddSingleton<CreateViewModel<RentalContractHomeVMUI>>(services => () => services.GetRequiredService<RentalContractHomeVMUI>());
                services.AddSingleton<CreateViewModel<BillHomeVMUI>>(services => () => services.GetRequiredService<BillHomeVMUI>());
                services.AddSingleton<CreateViewModel<DepositContractHomeVMUI>>(services => () => services.GetRequiredService<DepositContractHomeVMUI>());
                services.AddSingleton<CreateViewModel<AddCustomerVMUI>>(services => () => services.GetRequiredService<AddCustomerVMUI>());
                services.AddSingleton<CreateViewModel<RoomDeleteVMUI>>(services => () => services.GetRequiredService<RoomDeleteVMUI>());
                services.AddSingleton<CreateViewModel<RoomDetailsAddImageVMUI>>(services => () => services.GetRequiredService<RoomDetailsAddImageVMUI>());
                services.AddSingleton<CreateViewModel<RoomDetailsEnlarge>>(services => () => services.GetRequiredService<RoomDetailsEnlarge>());
                services.AddSingleton<CreateViewModel<FurnitureAddVMUI>>(services => () => services.GetRequiredService<FurnitureAddVMUI>());
                services.AddSingleton<CreateViewModel<RentalContractAddVMUI>>(services => () => services.GetRequiredService<RentalContractAddVMUI>());
                services.AddSingleton<CreateViewModel<RentalContractUpdateVMUI>>(services => () => services.GetRequiredService<RentalContractUpdateVMUI>());
                services.AddSingleton<CreateViewModel<AddCustomerInRentalVMUI>>(services => () => services.GetRequiredService<AddCustomerInRentalVMUI>());
                services.AddSingleton<CreateViewModel<DepositContractAddVMUI>>(services => () => services.GetRequiredService<DepositContractAddVMUI>());
                services.AddSingleton<CreateViewModel<DepositContractUpdateVMUI>>(services => () => services.GetRequiredService<DepositContractUpdateVMUI>());
                services.AddSingleton<CreateViewModel<FurnitureUpdateVMUI>>(services => () => services.GetRequiredService<FurnitureUpdateVMUI>());
                services.AddSingleton<CreateViewModel<RoomDetailsInformationCustomerVMUI>>(services => () => services.GetRequiredService<RoomDetailsInformationCustomerVMUI>());
                services.AddSingleton<CreateViewModel<BillAddVMUI>>(services => () => services.GetRequiredService<BillAddVMUI>());
                services.AddSingleton<CreateViewModel<BillUpdateVMUI>>(services => () => services.GetRequiredService<BillUpdateVMUI>());
                services.AddSingleton<CreateViewModel<StatisticsHomeVMUI>>(services => () => services.GetRequiredService<StatisticsHomeVMUI>());

                /*-------------------------------------------------------------------------------*/
                services.AddSingleton<ViewModelDelegateRenavigator<RoomHomeVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<HomeVM>>();
                services.AddSingleton<ViewModelDelegateRenavigator<CustomerVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RoomAddVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RoomUpdateVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RoomDetailsHomeVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<FurnitureHomeVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RentalContractHomeVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<BillHomeVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<DepositContractHomeVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<AddCustomerVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RoomDeleteVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RoomDetailsAddImageVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RoomDetailsEnlarge>>();
                services.AddSingleton<ViewModelDelegateRenavigator<FurnitureAddVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<FurnitureUpdateVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RentalContractAddVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<RentalContractUpdateVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<AddCustomerInRentalVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<DepositContractAddVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<DepositContractUpdateVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<BillAddVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<BillUpdateVMUI>>();
                services.AddSingleton<ViewModelDelegateRenavigator<StatisticsHomeVMUI>>();
                services.AddScoped<RoomUpdateViewModel>();
            });

            return host;
        }
    }
}