using Data.Entity;
using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Relationships
{
    public class PeopleRental : DomainObject
    {
        public int IDPeople { get; set; }

        public int IDRental { get; set; }

        public Membership Membership { get; set; }

        public People? People { get; set; }

        public RentalContract? RentalContract { get; set; }


    }
}
