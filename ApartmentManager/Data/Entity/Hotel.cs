using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Hotel
    {
        private string v;

        public Hotel(string v)
        {
            this.v = v;
        }

        public string name { get; set; }
    }
}
