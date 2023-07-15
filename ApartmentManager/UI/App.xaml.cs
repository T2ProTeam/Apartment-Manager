using AM.UI;
using AM.UI.Model;
using AM.UI.StartupHelper;
using Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Implement;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using AM.UI.ViewModelUI;

namespace AM.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<Login>();
                    services.AddSingleton<MainWindow>();
                    services.AddScoped<NavigationVM>();
                    //   services.AddFormFactory<MainWindow>();
                    services.AddSingleton<View.People.Home>();
                    services.AddTransient<IPeople, PeopleServices>();
                    services.AddTransient<IBill, BillServices>();
                }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            var starupForm = AppHost.Services.GetRequiredService<MainWindow>();
            //starupForm.DataContext = AppHost.Services.GetRequiredService<NavigationVM>();
            starupForm.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }
}