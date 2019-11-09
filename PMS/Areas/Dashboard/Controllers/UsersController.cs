using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PMS.Areas.Dashboard.ViewModels;
using PMS.Entities;
using PMS.Services;
using PMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<ActionResult> Action(string ID)
        {
           UserActionModel model = new UserActionModel();

            if (string.IsNullOrEmpty(ID)) //we are trying to edit a record
            {
                var user = await UserManager.FindByIdAsync(ID);

                model.ID = user.Id;
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.UserName = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;
            }

            

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<JsonResult> Action(UserActionModel model)
        {
            JsonResult json = new JsonResult();

            IdentityResult result = null;

            if  (string.IsNullOrEmpty(model.ID)) //we are trying to edit a record
                {
                var user = await UserManager.FindByIdAsync(model.ID);

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;

                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

               result = await UserManager.UpdateAsync(user);

              

            }
            else //we are trying to create a record
            {
                var user = new PMSUser();

                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;

                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;

                result = await UserManager.CreateAsync(user);
            }

          
            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

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