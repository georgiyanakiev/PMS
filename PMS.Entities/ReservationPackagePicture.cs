using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Entities
{
    public class ReservationPackagePicture
    {
        public int ID { get; set; }
        public int ReservationPackageID { get; set; }
        public int PictureID { get; set; }
        public virtual Picture Picture { get; set; }
    }
}
