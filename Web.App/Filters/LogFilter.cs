using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.App.Filters
{

    public class LogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string logMessage = $"Controller: {controllerName}, Action: {actionName}, Start Time: {DateTime.Now}";

            // Loglama kodunu burada gerçekleştirin (Trace.WriteLine, Log4Net, Elmah, vb.)
             Trace.WriteLine(logMessage);//ctrl+alt+o yapıp output da logları görebiliriz.

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            string logMessage = $"Controller: {controllerName}, Action: {actionName}, End Time: {DateTime.Now}";

            // Loglama kodunu burada gerçekleştirin

            base.OnActionExecuted(filterContext);
        }
    }
}