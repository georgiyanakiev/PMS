using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class AccommodationsViewModels
    {   
        public AccommodationType AccommodationType { get; set; }
        public IEnumerable<Accommodation> Accommodations { get; set; }
        public string SearchTerm { get; set; }
        public int? AccommodationPackageID { get; set; }
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public Pager Pager { get; set; }
        public int SelectedAccommodationPackageID { get; set; }
    }
}