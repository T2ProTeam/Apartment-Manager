using Data.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    public class PeopleRentalConfiguration : IEntityTypeConfiguration<PeopleRental>
    {
        public void Configure(EntityTypeBuilder<PeopleRental> builder)
        {
            builder.ToTable("PeopleRental");
            builder.HasKey(x => x.ID);
            builder.HasOne(x => x.People).WithMany(x => x.PeopleRental).HasForeignKey(x => x.IDPeople);
            builder.HasOne(x => x.RentalContract).WithMany(x => x.PeopleRental).HasForeignKey(x => x.IDRental);
        }
    }
}
