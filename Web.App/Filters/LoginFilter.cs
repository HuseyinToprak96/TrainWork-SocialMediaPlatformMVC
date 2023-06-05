using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.App.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? userID=1; //=Session["ID"];
            HttpCookie cookie = new HttpCookie("MyCookies");
            RequestContext requestContext = new RequestContext();
            
            //int id = Convert.ToInt32(Session["Id"]);
            if (Convert.ToInt32(cookie.Values["UserId"])==0)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"action","KullaniciGiris" },
                    { "controller","Login"}
                });
            }
            base.OnActionExecuting(context);
        }
    }
}