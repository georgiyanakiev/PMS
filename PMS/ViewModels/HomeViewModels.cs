using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<AccommodationType> AccommodationTypes { get; set; }
        public IEnumerable<AccommodationPackage> AccommodationPackages { get; internal set; }
    }
}