using Data.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Furniture : DomainObject
    {
        public string Name { get; set; }

        public List<RoomDetails> RoomDeltails { get; } = new List<RoomDetails>();
    }
}