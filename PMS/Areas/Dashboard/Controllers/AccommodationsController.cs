using PMS.Areas.Dashboard.ViewModels;
using PMS.Entities;
using PMS.Services;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.Dashboard.Controllers
{
    public class AccommodationsController : Controller
    {
        AccommodationPackagesService accommodationPackagesService = new AccommodationPackagesService();
        AccommodationService accommodationsService = new AccommodationService();

        public ActionResult Index(string searchTerm, int? accommodationPackageID, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;

            ViewModels.AccommodationViewModels model = new ViewModels.AccommodationViewModels();

            model.SearchTerm = searchTerm;
            model.AccommodationPackageID = accommodationPackageID;
            model.AccommodationPackages = accommodationPackagesService.GetAllAccommodationPackages();

            model.Accommodations = accommodationsService.SearchAccommodations(searchTerm, accommodationPackageID, page.Value, recordSize);
            var totalRecords = accommodationsService.SearchAccommodationsCount(searchTerm, accommodationPackageID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationActionModel model = new AccommodationActionModel();

            if (ID.HasValue) 
            {
                var accommodation = accommodationsService.GetAccommodationByID(ID.Value);

                model.ID = accommodation.ID;
                model.AccommodationPackageID = accommodation.AccommodationPackageID;
                model.Name = accommodation.Name;
                model.Description = accommodation.Description;
            }

            model.AccommodationPackages = accommodationPackagesService.GetAllAccommodationPackages();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccommodationActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            if (model.ID > 0) 
            {
                var accommodation = accommodationsService.GetAccommodationByID(model.ID);

                accommodation.AccommodationPackageID = model.AccommodationPackageID;
                accommodation.Name = model.Name;
                accommodation.Description = model.Description;

                result = accommodationsService.UpdateAccommodation(accommodation);
            }
            else 
            {
                Accommodation accommodation = new Accommodation();

                accommodation.AccommodationPackageID = model.AccommodationPackageID;
                accommodation.Name = model.Name;
                accommodation.Description = model.Description;

                result = accommodationsService.SaveAccommodation(accommodation);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accommodation." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccommodationActionModel model = new AccommodationActionModel();

            var accommodation = accommodationsService.GetAccommodationByID(ID);

            model.ID = accommodation.ID;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccommodationActionModel model)
        {
            JsonResult json = new JsonResult();

            var result = false;

            var accommodation = accommodationsService.GetAccommodationByID(model.ID);

            result = accommodationsService.DeleteAccommodation(accommodation);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accommodation." };
            }

            return json;
        }
    }
}