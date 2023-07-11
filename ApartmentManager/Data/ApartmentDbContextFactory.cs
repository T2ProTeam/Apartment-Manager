﻿using Microsoft.EntityFrameworkCore.Design;
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
    public class ApartmentDbContextFactory : IDesignTimeDbContextFactory<AparmentDbContext>
    {
        public AparmentDbContext CreateDbContext(string[] args = null)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AparmentDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-IQ3UV32\\SQLEXPRESS;Database=Apartment;User ID=sa;Password=123456;TrustServerCertificate=True");
            return new AparmentDbContext(optionsBuilder.Options);
        }
    }
}