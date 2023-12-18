using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Statistics : DomainObject
    {
        public int ElectricMoneyOfGovernment { get; set; }
        public int WaterMoneyOfGovernment { get; set; }
        public int ServiceOfGovernment { get; set; }

        public int Month { get; set; }
        public int Year { get; set; }
    }
}