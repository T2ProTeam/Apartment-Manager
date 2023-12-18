using Data.Entity;
using Data.Relationships;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class RoomDetailsConfiguration : IEntityTypeConfiguration<RoomDetails>
    {
        public void Configure(EntityTypeBuilder<RoomDetails> builder)
        {
            builder.ToTable("RoomDetails");
            builder.HasKey(x => new { x.IDFur, x.IDRoom });
            builder.HasOne(x => x.Furniture).WithMany(x => x.RoomDeltails).HasForeignKey(x => x.IDFur);
            builder.HasOne(x => x.Room).WithMany(x => x.RoomDeltails).HasForeignKey(x => x.IDRoom);
        }
    }
}