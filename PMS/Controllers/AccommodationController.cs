using PMS.Entities;
using PMS.Services;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace PMS.Controllers
{
    public class AccommodationController : Controller
    {
        AccommodationTypesService accommodationTypesService = new AccommodationTypesService();
        AccommodationPackagesService accommodationPackagesService = new AccommodationPackagesService();
        AccommodationService accommodationsService = new AccommodationService();

        public ActionResult Index(int accommodationTypeID, int? accommodationPackageID)
        {
            AccommodationsViewModels model = new AccommodationsViewModels();

            model.AccommodationType = accommodationTypesService.GetAccommodationTypeByID(accommodationTypeID);

            model.AccommodationPackages = accommodationPackagesService.GetAllAccommodationPackagesByAccommodationType(model.Accommodation.First().ID);

            model.Accommodations = accommodationsService.GetAllAccommodationsByAccommodationPackage(model.SelectedAccommodationPackageID);

            return View(model);
        }

        //public ActionResult Details(int accommodationPackageID)
        //{
        //    AccommodationPackageDetailsViewModel model = new AccommodationPackageDetailsViewModel();

        //    model.AccomodationPackage = accommodationPackagesService.GetAccommodationPackageByID(accommodationPackageID);

        //    return View(model);
        //}

        //public ActionResult CheckAvailability(CheckAccommodationAvailabilityViewModel model)
        //{
        //    return View();
        //}
    }
}