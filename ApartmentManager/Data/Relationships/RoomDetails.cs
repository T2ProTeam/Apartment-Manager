using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace Data.Relationships
{
    [Keyless]
    public class RoomDetails 
    {
        public int IDFur { get; set; }
        public int IDRoom { get; set; }

        public Furniture? Furniture { get; set; }
        public Room? Room { get; set; }

    }
}