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
    public class RoomImageConfiguration : IEntityTypeConfiguration<RoomImage>
    {
        public void Configure(EntityTypeBuilder<RoomImage> builder)
        {
            builder.ToTable("RoomImage");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.HasOne(x => x.Room).WithMany(x => x.RoomImage).HasForeignKey(x => x.IDroom);
            builder.Property(x => x.Url).HasMaxLength(300).IsRequired();
        }
    }
}