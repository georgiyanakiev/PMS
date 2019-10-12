using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Entities
{
    public class HotelRoom
    {
        public int ID { get; set; }
        public int AccommodationTypeID{ get; set; }
        public int RoomNo { get; set; }
        public int NoOfBeds { get; set; }
        public string Details { get; set; }
        public bool HasBath { get; set; }
    }
}
