using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.Areas.Dashboard.ViewModels
{
    public class AccommodationTypesListingModels
    {
        public IEnumerable<AccommodationType> AccommodationTypes{ get; set; }
        public string SearchTerm { get; set; }
        public int? AccommodationTypeID { get; set; }
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; internal set; }
    }

    public class AccommodationTypeActionModels
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}