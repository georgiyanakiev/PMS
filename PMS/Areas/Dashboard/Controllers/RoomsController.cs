using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.Dashboard.Controllers
{
    public class RoomsController : Controller
    {
        // GET: Dashboard/Rooms
        public ActionResult Index()
        {
            return View();
        }
    }
}