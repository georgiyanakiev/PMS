using Microsoft.AspNet.Identity.Owin;
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
    public class UsersController : Controller
    {
        private PMSSignInManager _signInManager;
        private PMSUserManager _userManager;
        public PMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<PMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public PMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<PMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public UsersController()
        {
        }

        public UsersController(PMSUserManager userManager, PMSSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        AccommodationPackagesService accommodationPackagesService = new AccommodationPackagesService();
        AccommodationService accommodationsService = new AccommodationService();

        public ActionResult Index(string searchTerm, string roleID, int? page)
        {
            int recordSize = 1;
            page = page ?? 1;

            UsersListingModel model = new UsersListingModel();

            model.SearchTerm = searchTerm;
            model.RoleID = roleID;
            //model.Roles = accommodationPackagesService.GetAllAccommodationPackages();

            model.Users = SeаrchUsers(searchTerm, roleID, page.Value, recordSize);
            var totalRecords = SeаrchUsersCount(searchTerm, roleID);

            model.Pager = new Pager(totalRecords, page, recordSize);

            return View(model);
        }
        public IEnumerable<PMSUser> SeаrchUsers(string searchTerm, string roleID, int page, int recordSize)

        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                //users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * recordSize;

            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public int SeаrchUsersCount(string searchTerm, string roleID)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrEmpty(roleID))
            {
                //users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }



            return users.Count();
        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccommodationActionModel model = new AccommodationActionModel();

            if (ID.HasValue) //we are trying to edit a record
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

            if (model.ID > 0) //we are trying to edit a record
            {
                var accommodation = accommodationsService.GetAccommodationByID(model.ID);

                accommodation.AccommodationPackageID = model.AccommodationPackageID;
                accommodation.Name = model.Name;
                accommodation.Description = model.Description;

                result = accommodationsService.UpdateAccommodation(accommodation);
            }
            else //we are trying to create a record
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