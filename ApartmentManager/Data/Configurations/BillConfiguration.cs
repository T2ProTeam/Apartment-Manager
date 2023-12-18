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
    public class BillConfiguration : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bill");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired();
            builder.HasOne(x => x.RentalContract).WithMany(x => x.Bills).HasForeignKey(x => x.IDRTC);
            builder.Property(x => x.ElectricQuantity).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Active).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PayDate).HasMaxLength(50);
            builder.Property(x => x.TotalMoney).IsRequired();
        }
    }
}