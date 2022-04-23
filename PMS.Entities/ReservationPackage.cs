using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Entities
{
    public class ReservationPackage
    {
        public int ID { get; set; }

        public int? ReservationTypeID { get; set; }
        public virtual ReservationType ReservationType { get; set; }

        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public int ReservationNumber { get; set; }
        public decimal FeePerNight { get; set; }
        public virtual List<ReservationPackagePicture> ReservationPackagePicture { get; set; }
    }
}
