using Data.Enum;
using Data.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entity
{
    public class Room : DomainObject
    {
        public string Name { get; set; }
        public int Quantity { get; set; }

        public int Staked { get; set; }

        public ICollection<RoomDetails> RoomDeltails { get; } = new List<RoomDetails>();

        public ICollection<RoomImage> RoomImage { get; } = new List<RoomImage>();

        public ICollection<RentalContract> RentalContracts { get; } = new List<RentalContract>();
        public ICollection<DepositsContract> DepositsContracts { get; } = new List<DepositsContract>();
    }
}