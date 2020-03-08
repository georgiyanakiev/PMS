using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PMS.Areas.Dashboard.Controllers
{
    public class ReservationsController : Controller
    {
        // GET: Dashboard/Reservations
        public ActionResult Index()
        {
            return View();
        }
    }
}