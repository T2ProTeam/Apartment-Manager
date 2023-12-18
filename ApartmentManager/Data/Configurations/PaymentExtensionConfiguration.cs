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
    public class PaymentExtensionConfiguration : IEntityTypeConfiguration<PaymentExtension>
    {
        public void Configure(EntityTypeBuilder<PaymentExtension> builder)
        {
            builder.ToTable("PaymentExtension");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired();
            builder.HasOne(x => x.Bill).WithMany(x => x.PaymentExtensions).HasForeignKey(x => x.IDBill);
            builder.Property(x => x.Days).HasDefaultValue(DateTime.Now);
        }
    }
}