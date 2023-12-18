using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Data
{
    public class ApartmentDbContextFactory
    {
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;


        public ApartmentDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public AparmentDbContext CreateDbContext()
        {
            DbContextOptionsBuilder<AparmentDbContext> options = new DbContextOptionsBuilder<AparmentDbContext>();
            _configureDbContext(options);
            return new AparmentDbContext(options.Options);
        }
    }
}