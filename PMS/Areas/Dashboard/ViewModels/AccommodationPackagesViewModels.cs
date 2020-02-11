using PMS.ViewModels;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.Dashboard.ViewModels
{
    public class AccommodationPackagesListingModels
    {
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }
        public int? AccommodationTypeID { get; set; }

        public Pager Pager { get; set; }
    }

    public class AccommodationPackageActionModels
    {
        public int ID { get; set; }
        public int? AccommodationTypeID{ get; set; }
        public AccommodationType AccommodationType { get; set; }

        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public string PictureIDs { get; set; }

        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }
        public List<AccommodationPackagePicture> AccommodationPackagePictures { get; set; }
    }
}