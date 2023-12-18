using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.UI.HostBuilderExtension
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {
                string connectionString = context.Configuration.GetConnectionString("ApartmentDB");
                Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlServer(connectionString, builder => builder.EnableRetryOnFailure());
                services.AddDbContext<AparmentDbContext>(configureDbContext);
                services.AddSingleton<ApartmentDbContextFactory>(new ApartmentDbContextFactory(configureDbContext));
            });

            return host;
        }
    }
}