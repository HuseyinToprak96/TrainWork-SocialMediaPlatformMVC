using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Web.App.Filters
{
    public class LogoutFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!FormsAuthentication.IsEnabled)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary{
                    {"action","Index" },
                    { "controller","Home"}
                });
            }
            base.OnActionExecuting(context);
        }
    }
}