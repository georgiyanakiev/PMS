using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Entities
{
    public class ReservationPicture
    {
        public int ID { get; set; }
        public int ReservationID { get; set; }
        public int PictureID { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
