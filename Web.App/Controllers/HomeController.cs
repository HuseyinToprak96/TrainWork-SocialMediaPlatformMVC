using CoreLayer.Dtos.Shared;
using CoreLayer.Dtos.User;
using Newtonsoft.Json;
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
            var shareds = await _sharedService.HomeSharedList(userId);
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

        public async Task<JsonResult> GetSharedComments(int id)
        {
            var datas=await _sharedService.GetSharedCommentList(id);
            return Json(datas.Data,JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddNewComment(string json)
        {
            CommentAddDto data=JsonConvert.DeserializeObject<CommentAddDto>(json);
            data.UserId = Convert.ToInt32(Session["UserId"]);
            var result= await _sharedService.CommentAddDto(data);
            return Json(result);
        }

        public async Task<JsonResult> Follow(int id)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            UserFollowDto userFollowDto = new UserFollowDto { FollowerId = userId, FollowingId = id };
            var result= await _userService.UserFollow(userFollowDto);
            if(result.Data)
            return Json(1);
            else
            return Json(result.Errors);
        }

        public async Task<JsonResult> SharedRepeat(int id)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result=await _sharedService.SharedRepeat(id, userId);
            if(result.Data)
            return Json(1);
            return Json(result.Errors);
        }

        public async Task<JsonResult> LikeShared(int SharedId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result=await _sharedService.AddLike(new SharedLikeDto { SharedId = SharedId , UserId=userId});
            if (result.StatusCode==200)
            return Json(1);
            else if(result.StatusCode==201)
                return Json(-1);
            return Json(result.Errors);
        }
    }
}