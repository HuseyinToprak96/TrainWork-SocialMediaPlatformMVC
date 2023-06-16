using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.App.Controllers
{
    [AllowAnonymous]
    public class BaseController : Controller
    {
        protected SharedService _sharedService = new SharedService();
        protected UserService _userService = new UserService();
        protected NotificationService _notificationService = new NotificationService();
        protected MailService _mailService = new MailService();
        protected PageService _pageService = new PageService();

    }
}