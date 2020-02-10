using PMS.ViewModels;
using PMS.Entities;
using PMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class AccommodationsController : Controller
    {
        AccommodationTypesService accommodationTypesService = new AccommodationTypesService();
        AccommodationPackagesService accommodationPackagesService = new AccommodationPackagesService();
        AccommodationsService accommodationsService = new AccommodationsService();

        public ActionResult Index(int accommodationTypeID, int? accommodationPackageID)
        {
            AccommodationsViewModels model = new AccommodationsViewModels();
            model.AccommodationType = accommodationTypesService.GetAccommodationTypeByID(accommodationTypeID);
            model.AccommodationPackages = accommodationPackagesService.GetAllAccommodationPackagesByAccommodationType(accommodationTypeID);
            model.Accommodations = accommodationsService.GetAllAccommodationsByAccommodationPackage(model.SelectedAccommodationPackageID);
            
            model.SelectedAccommodationPackageID = accommodationPackageID.HasValue ? accommodationPackageID.Value : model.AccommodationPackages.First().ID;

            return View(model);
        }

        //public ActionResult Details(int accommodationPackageID)
        //{
        //    AccommodationPackageDetailsViewModel model = new AccommodationPackageDetailsViewModel();

        //    model.AccommodationPackage = accommodationPackagesService.GetAccommodationPackageByID(accommodationPackageID);

        //    return View(model);
        //}

        //public ActionResult CheckAvailability(CheckAccommodationAvailabilityViewModel model)
        //{
        //    return View();
        //}
    }
}