using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Web.App.Filters
{
    public class LoginFilter : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                // Kullanıcı oturum açmışsa yetkilendirme başarılıdır
                return true;
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                // Ajax isteği ise, 401 Unauthorized hatası döndür
                filterContext.HttpContext.Response.StatusCode = 401;
                filterContext.HttpContext.Response.End();
            }
            else
            {
                // Diğer durumlarda, giriş sayfasına yönlendir
                filterContext.Result = new RedirectResult("/Auth/Login");
            }
        }
    }
}