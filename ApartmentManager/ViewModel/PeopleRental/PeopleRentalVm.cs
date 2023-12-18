using Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.PeopleRental
{
    public class PeopleRentalVm
    {
        public string PeopleName { get; set; }

        public int IDRental { get; set; }

        public Membership Membership { get; set; }
    }
}