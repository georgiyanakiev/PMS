using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Entities
{
   public class Reservation
    {
        public int ID { get; set; }
        public int ReservationID { get; set; }
        public virtual Reservation Reservations { get; set; }
        public string Name { get; set; }
        public int ReservationNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }

        public List<ReservationPicture> ReservationPicture { get; set; }



    }
}
