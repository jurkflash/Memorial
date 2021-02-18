using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Memorial
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SiteId",
                url: "{controller}/{action}/{siteId}",
                defaults: new { controller = "Home", action = "Index", siteId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ItemId",
                url: "{controller}/{action}/{itemId}",
                defaults: new { controller = "Home", action = "Index", itemId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "CremationOrders",
                url: "{controller}/{cremationItemId}/{applicantId}",
                defaults: new { controller = "CremationOrders", cremationItemId = UrlParameter.Optional, applicantId = UrlParameter.Optional }
            );
        }
    }
}
