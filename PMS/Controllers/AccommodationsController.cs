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
    public class AccomodationsController : Controller
    {
        AccommodationTypesService accommodationTypesService = new AccommodationTypesService();
        AccommodationPackagesService accommodationPackagesService = new AccommodationPackagesService();
        AccommodationsService accomodationsService = new AccommodationsService();

        public ActionResult Index(int accommodationTypeID, int? accommodationPackageID)
        {
            AccommodationsViewModels model = new AccommodationsViewModels();
            model.AccommodationPackages = accommodationPackagesService.GetAllAccommodationPackagesByAccommodationType(accommodationTypeID);
            model.AccommodationType = accommodationTypesService.GetAccommodationTypeByID(accommodationTypeID);
            model.SelectedAccommodationPackageID = accommodationPackageID.HasValue ? accommodationPackageID.Value : model.AccommodationPackages.First().ID;
            model.Accommodations = accomodationsService.GetAllAccommodationsByAccommodationPackage(model.SelectedAccommodationPackageID);

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