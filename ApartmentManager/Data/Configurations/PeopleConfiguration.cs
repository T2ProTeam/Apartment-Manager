﻿using Data.Enum;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class PeopleConfiguration : IEntityTypeConfiguration<People>
    {
        public void Configure(EntityTypeBuilder<People> builder)
        {
            builder.ToTable("People");
            builder.HasKey(x => x.ID);
            builder.Property(x=>x.ID).IsRequired();
            builder.HasOne(x=>x.Room).WithMany(x=>x.People).HasForeignKey(x=>x.IDroom);
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Sex).HasDefaultValue(Sex.Male);
            builder.Property(x=>x.Birthday).HasDefaultValue(DateTime.Now);
            builder.Property(x=>x.Email).HasMaxLength(50);
            builder.Property(x=>x.PhoneNumber).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.IDCard).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Address).IsRequired().HasMaxLength(50);
        }
    }
}