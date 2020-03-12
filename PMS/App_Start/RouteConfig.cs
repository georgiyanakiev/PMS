using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FEAccommodations",
                url: "Accommodations",
                defaults: new { area = "", controller = "Accommodation", action = "Index" },
                namespaces: new[] { "PMS.Controllers" }
            );

            routes.MapRoute(
                name: "AccommodationPackageDetails",
                url: "accommodation-package/{accommodationPackageID}",
                defaults: new { area = "", controller = "Accommodation", action = "Details" },
                namespaces: new[] { "PMS.Controllers" }
            );

            routes.MapRoute(
                name: "CheckAvailability",
                url: "accommodation-check-availability",
                defaults: new { area = "", controller = "Accommodation", action = "CheckAvailability" },
                namespaces: new[] { "PMS.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
