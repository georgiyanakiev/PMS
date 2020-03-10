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
        public int AccommodationPackageID { get; set; }
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; set; }
        public Pager Pager { get; set; }
        public int SelectedAccommodationPackageID { get; set; }
    }
    public class AccommodationPackageDetailsViewModel
    {
        public AccommodationPackage AccommodationPackage { get; set; }
    }
    public class CheckAccommodationAvailabilityViewModel
    {
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }
}