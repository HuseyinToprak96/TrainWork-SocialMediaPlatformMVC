using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Web.App.Filters
{
    public class AdminFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {


            //int id = Convert.ToInt32(Session["Id"]);
            var data = FormsAuthentication.GetAuthCookie("RoleId", false);
            if (true)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"action","Login" },
                    { "controller","Auth"}
                });
            }
            base.OnActionExecuting(context);
        }
    }
}