using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class RentalContractConfiguration : IEntityTypeConfiguration<RentalContract>
    {
        public void Configure(EntityTypeBuilder<RentalContract> builder)
        {
            builder.ToTable("RentalContract");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired();
            builder.HasOne(x => x.Room).WithMany(x => x.RentalContracts).HasForeignKey(x => x.IDroom);
            builder.Property(x => x.ReceiveDate).HasMaxLength(50);
            builder.Property(x => x.CheckOutDate).HasMaxLength(50);
            builder.Property(x => x.RoomMoney).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ElectricMoney).HasMaxLength(50).IsRequired();
            builder.Property(x => x.WaterMoney).HasMaxLength(50).IsRequired();
            builder.Property(x => x.ServiceMoney).HasMaxLength(50).IsRequired();
        }
    }
}