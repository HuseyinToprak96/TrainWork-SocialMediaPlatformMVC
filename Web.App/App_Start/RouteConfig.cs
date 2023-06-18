using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.App
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
          name: "About",
          url: "Hakkimizda/{route}",
          defaults: new { controller = "Home", action = "Page" }
      );
            routes.MapRoute(
        name: "Notification",
        url: "Bildirim/{shared}/{href}",
        defaults: new { controller = "Notification", action = "Get" }
   );
            routes.MapRoute(
      name: "NotificationFollow",
      url: "Bildirim/{follow}",
      defaults: new { controller = "Notification", action = "Get2" }
 );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { area="" ,controller = "Auth", action = "Login", id = UrlParameter.Optional }
            );
        }
    }
}
