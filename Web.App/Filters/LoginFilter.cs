﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Web.App.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var data = FormsAuthentication.GetAuthCookie("RoleId", false);
            data.Value = data.Value.Trim();
            //int id = Convert.ToInt32(Session["Id"]);
            if (!FormsAuthentication.IsEnabled)
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