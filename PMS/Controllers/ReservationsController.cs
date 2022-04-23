using PMS.Services;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Controllers
{
    public class ReservationsController : Controller
    {
        ReservationTypesService reservationTypesService = new ReservationTypesService();
        ReservationPackagesService reservationPackagesService = new ReservationPackagesService();
        ReservationsService reservationsService = new ReservationsService();

        public ActionResult Index(int reservationTypeID, int? reservationPackageID)
        {
            ReservationsViewModels model = new ReservationsViewModels();

            model.ReservationType = reservationTypesService.GetReservationTypeByID(reservationTypeID);

            model.ReservationPackages = reservationPackagesService.GetAllReservationPackagesByReservationType(reservationTypeID);

            model.SelectedReservationPackageID = reservationPackageID.HasValue ? reservationPackageID.Value : model.ReservationPackages.First().ID;

            model.Reservations = reservationsService.GetAllReservationsByReservationPackage(model.SelectedReservationPackageID);

            return View(model);
        }

        public ActionResult Details(int reservationPackageID)
        {
            ReservationPackageDetailsViewModel model = new ReservationPackageDetailsViewModel();

            model.ReservationPackage = reservationPackagesService.GetReservationPackageByID(reservationPackageID);

            return View(model);
        }

        public ActionResult CheckAvailability(int checkAvailabilityID)
        {
            CheckReservationAvailabilityViewModel model = new CheckReservationAvailabilityViewModel();

            model.ReservationsService = reservationsService.GetReservationByID(checkAvailabilityID);
   
            return View();
        }
    }
}