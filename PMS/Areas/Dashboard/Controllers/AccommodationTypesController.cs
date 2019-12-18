using PMS.Areas.Dashboard.ViewModels;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.Dashboard.Controllers
{
    public class AccommodationTypesController : Controller
    {
        AccommodationTypesService accommodationTypesService = new AccommodationTypesService();

        public ActionResult Index(string SearchTerm)
        {
            AccommodationTypesListingModels model = new AccommodationTypesListingModels();

            model.SearchTerm = SearchTerm;

            model.AccommodationTypes = accommodationTypesService.SearchAccommodationTypes(SearchTerm);

            
            return View(model);
        }
       
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationTypeActionModels model = new AccommodationTypeActionModels();

            if (ID.HasValue)
            {
                var accommodationType = accommodationTypesService.GetAccommodationTypeByID(ID.Value);

                model.ID = accommodationType.ID;
                model.Name = accommodationType.Name;
                model.Description = accommodationType.Description;
            }


            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationTypeActionModels model)
        {

            JsonResult json = new JsonResult();
            var result = false;
            if (model.ID > 0)
            {
                var accommodationType = accommodationTypesService.GetAccommodationTypeByID(model.ID);
                accommodationType.Name = model.Name;
                accommodationType.Description = model.Description;

                result = accommodationTypesService.UpdateAccommodationType(accommodationType);
            }
            else
            {
                AccommodationType accommodationType = new AccommodationType();

                accommodationType.Name = model.Name;
                accommodationType.Description = model.Description;



                result = accommodationTypesService.SaveAccommodationType(accommodationType);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Meesage = "Unable to perform action on Accommodation Types" };
            }
            return json;
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccommodationTypeActionModels model = new AccommodationTypeActionModels();

            var accommodationType = accommodationTypesService.GetAccommodationTypeByID(ID);

            model.ID = accommodationType.ID;


            return PartialView("_Delete", model);
        }
        [HttpPost]
        public JsonResult Delete(AccommodationTypeActionModels model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            var accommodationType = accommodationTypesService.GetAccommodationTypeByID(model.ID);
            result = accommodationTypesService.DeleteAccommodationType(accommodationType);
            

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Meesage = "Unable to perform action on Accommodation Types" };
            }
            return json;
        }
    }
}