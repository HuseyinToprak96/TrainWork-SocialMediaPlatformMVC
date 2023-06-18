using System;
using System.Web.Mvc;

public class AdminFilter : AuthorizeAttribute
{
    protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
    {
        // Yetkilendirme işlemini burada özelleştirin
        // Kullanıcının yetkisi varsa true, yoksa false döndürün
        // Örneğin, kullanıcının belirli bir role sahip olması gerekiyorsa:
        var data = httpContext.User;
        if (httpContext.User.IsInRole("Admin"))
        {
            return true;
        }

        return false;
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        // Yetkilendirme başarısız olduğunda yönlendirme yapılacak sayfayı burada belirtin
        filterContext.Result = new RedirectResult("~/Error/Unauthorized");
    }
}
