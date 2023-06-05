using CoreLayer.Dtos.Shared;
using CoreLayer.Dtos.User;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.App.Filters;

namespace Web.App.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SharedService _sharedService = new SharedService();
        private UserService _userService= new UserService(); 
        public async Task<ActionResult> Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var shareds = await _sharedService.HomeSharedList();
            var users = await _userService.GetRecommendedPeople(userId);
            return View(Tuple.Create<IEnumerable<SharedListDto>,IEnumerable<RecommendedPeopleDto>>(shareds.Data,users.Data));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}