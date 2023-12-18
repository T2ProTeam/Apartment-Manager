using Data.Enum;
using Data.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class People : DomainObject
    {
        public string Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string IDCard { get; set; }
        public string Address { get; set; }

        public ICollection<PeopleRental> PeopleRental { get; } = new List<PeopleRental>();

        public ICollection<RentalContract> RentalContracts { get; } = new List<RentalContract>();

        public ICollection<DepositsContract> DepositsContracts { get; } = new List<DepositsContract>();
    }
}