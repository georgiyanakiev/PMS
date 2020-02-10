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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            AccommodationTypesService service = new AccommodationTypesService();
            AccommodationPackagesService accomodationPackagesService = new AccommodationPackagesService();

            model.AccommodationTypes = service.GetAllAccommodationTypes();
            model.AccommodationPackages = accomodationPackagesService.GetAllAccommodationPackages();

            
            return View(model);
        }

       
    }
}