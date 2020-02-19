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
    public class AccommodationPackagesController : Controller
    {
       AccommodationPackagesService accommodationPackagesService = new AccommodationPackagesService();
       AccommodationTypesService accommodationTypesService = new AccommodationTypesService();

        DashboardService dashboardService = new DashboardService();
        public ActionResult Index(string searchTerm,int? accommodationTypeID, int? page)
        {

            int recordSize = 5;
            page = page ?? 1;

            AccommodationPackagesListingModels model = new AccommodationPackagesListingModels();

            model.SearchTerm = searchTerm;
            model.AccommodationTypeID = accommodationTypeID;

            model.AccommodationPackages = accommodationPackagesService.SearchAccommodationPackagesCount(searchTerm, accommodationTypeID, page.Value, recordSize);

            model.AccommodationTypes = accommodationTypesService.GetAllAccommodationTypes();

            var totalRecords = accommodationPackagesService.SearchAccommodationPackagesCount(searchTerm, accommodationTypeID);

            model.Pager = new Pager(totalRecords, page,recordSize);



            return View(model);
        }


        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationPackageActionModels model = new AccommodationPackageActionModels();
            
            if (ID.HasValue) //edti record 
            {
                var accommodationPackage = accommodationPackagesService.GetAccommodationPackageByID(ID.Value);

                model.ID = accommodationPackage.ID;
                model.AccommodationTypeID = accommodationPackage.AccommodationTypeID;
                model.Name = accommodationPackage.Name;
                model.NoOfRoom = accommodationPackage.NoOfRoom;
                model.FeePerNight = accommodationPackage.FeePerNight;
                model.AccommodationPackagePictures = accommodationPackagesService.GetPicturesByAccommodationPackageID(accommodationPackage.ID);
            }

            model.AccommodationTypes = accommodationTypesService.GetAllAccommodationTypes();

            return PartialView("_Action", model);
        }
        [HttpPost]
        public JsonResult Action(AccommodationPackageActionModels model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            //model.PictureIDs = "90,67,23" = ["90", "67", "23"] = {90, 67, 23}
            List<int> pictureIDs = !string.IsNullOrEmpty(model.PictureIDs) ? model.PictureIDs.Split(',').Select(x => int.Parse(x)).ToList() : new List<int>();

            var pictures = dashboardService.GetPicturesByIDs(pictureIDs);

            if (model.ID > 0) //trying to edit a record 
            {
                var accommodationPackage = accommodationPackagesService.GetAccommodationPackageByID(model.ID);

                accommodationPackage.AccommodationTypeID = model.AccommodationTypeID;
                accommodationPackage.Name = model.Name;
                accommodationPackage.NoOfRoom = model.NoOfRoom;
                accommodationPackage.FeePerNight = model.FeePerNight;

                accommodationPackage.AccommodationPackagePicture.Clear();
                accommodationPackage.AccommodationPackagePicture.AddRange(pictures.Select(x => new AccommodationPackagePicture() { AccommodationPackageID = accommodationPackage.ID, PictureID = x.ID }));
                result = accommodationPackagesService.UpdateAccommodationPackage(accommodationPackage);
            }
            else // trying to create a record 
            {
                AccommodationPackage accommodationPackage = new AccommodationPackage();

                accommodationPackage.AccommodationTypeID = model.AccommodationTypeID;
                accommodationPackage.Name = model.Name;
                accommodationPackage.NoOfRoom = model.NoOfRoom;
                accommodationPackage.FeePerNight = model.FeePerNight;



                accommodationPackage.AccommodationPackagePicture = new List<AccommodationPackagePicture>();
                accommodationPackage.AccommodationPackagePicture.AddRange(pictures.Select(x => new AccommodationPackagePicture() { PictureID = x.ID }));

                result = accommodationPackagesService.SaveAccommodationPackage(accommodationPackage);
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
            AccommodationPackageActionModels model = new AccommodationPackageActionModels();

            var accommodationPackage = accommodationPackagesService.GetAccommodationPackageByID(ID);
            
            model.ID = accommodationPackage.ID;
            


            return PartialView("_Delete", model);
        }
        [HttpPost]
        public JsonResult Delete(AccommodationPackageActionModels model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            var accommodationPackage = accommodationPackagesService.GetAccommodationPackageByID(model.ID);
            result = accommodationPackagesService.DeleteAccommodationPackage(accommodationPackage);


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