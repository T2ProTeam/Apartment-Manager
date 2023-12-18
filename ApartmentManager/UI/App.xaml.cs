using AM.UI.Model;
using AM.UI.Utilities;
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
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using AM.UI.ViewModelUI;
using AM.UI.HostBuilderExtension;
using Microsoft.EntityFrameworkCore;
using ViewModel.People;
using System.Net;

namespace AM.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddStores()
                .AddConfiguration()
                .AddServices()
                .AddViewModels()
                .AddDbContext()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddSingleton(s => new MainWindow()
                   {
                       DataContext = s.GetRequiredService<NavigationVM>()
                   });
               }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
    }
}