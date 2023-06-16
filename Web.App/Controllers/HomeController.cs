using CoreLayer.Dtos.Page;
using CoreLayer.Dtos.Shared;
using CoreLayer.Dtos.User;
using CoreLayer.Enum;
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
using WebGrease.Css.Extensions;

namespace Web.App.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Home")]
    public class HomeController : BaseController
    {
     
        public async Task<ActionResult> Index()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            if (userId > 0)
            {
                var shareds = await _sharedService.HomeSharedList(userId);
                var users = await _userService.GetRecommendedPeople(userId);
                return View(Tuple.Create<IEnumerable<SharedListDto>, IEnumerable<RecommendedPeopleDto>>(shareds.Data, users.Data));
            }
            return Redirect("/");
        }
        [HttpPost]
        public async Task<ActionResult> Index(SharedFilterDto sharedFilterDto)
        {
            
            int userId = Convert.ToInt32(Session["UserId"]);
            if (userId > 0)
            {
                var shareds = await _sharedService.GetSharedFilter(sharedFilterDto);
                var users = await _userService.GetRecommendedPeople(userId);
                return View(Tuple.Create<IEnumerable<SharedListDto>, IEnumerable<RecommendedPeopleDto>>(shareds.Data, users.Data));
            }
            return Redirect("/");
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

        public ActionResult NavbarLinks()
        {
            var links= _pageService.Where(x=>x.IsActive && !x.IsDeleted);
            List<PageNavLinkDto> pageNavLinkDtos= new List<PageNavLinkDto>();
            links.Data.ForEach(x =>
            {
                PageNavLinkDto pageNavLinkDto=new PageNavLinkDto { Id=x.Id, LinkName=x.LinkName, Route=x.Route};
                pageNavLinkDtos.Add(pageNavLinkDto);
            });
            return PartialView(pageNavLinkDtos);
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
            var sharedResult = await _sharedService.GetAsync(data.SharedId);
            var userResult = await _userService.GetAsync(data.UserId);
            if (data.TopCommentId == 0)
            {
                await _notificationService.AddAsync(new CoreLayer.Entities.Notification.Notification { NotificationType = ENotificationType.Comment, UserId = sharedResult.Data.UserId, Link = data.SharedId.ToString(), Content = userResult.Data.Username + " adlı kullanıcı " + sharedResult.Data.Title + " başlıklı gönderinize yorum yaptı." });
            }
            else
            {
                await _notificationService.AddAsync(new CoreLayer.Entities.Notification.Notification { NotificationType = ENotificationType.CommentAnswer, UserId = sharedResult.Data.UserId, Link = data.SharedId.ToString(), Content = userResult.Data.Username + " adlı kullanıcı yorumunuza cevap verdi." });
            }
            return Json(result.Data);
        }

        public async Task<JsonResult> Follow(int id)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            UserFollowDto userFollowDto = new UserFollowDto { FollowerId = userId, FollowingId = id };
            var result= await _userService.UserFollow(userFollowDto);
            if (result.Data)
            {
                var user = await _userService.GetAsync(userId);
                await _notificationService.AddAsync(new CoreLayer.Entities.Notification.Notification { NotificationType = ENotificationType.Follow, Link = userId.ToString(), UserId = id, Content = user.Data.Username + " adlı kullanıcı sizi takip etti." });
                return Json(1);
            }
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
            if (result.StatusCode == 200)
            {
                var sharedResult = await _sharedService.GetAsync(SharedId);
                var userResult=await _userService.GetAsync(userId);
                await _notificationService.AddAsync(new CoreLayer.Entities.Notification.Notification
                {
                    Link = SharedId.ToString(),
                    NotificationType = ENotificationType.Like,
                    UserId = sharedResult.Data.UserId,
                    Content = userResult.Data.Username+" adlı kullanıcı "+sharedResult.Data.Title+" başlıklı gönderinizi beğendi!"
                });
                return Json(1);
            }
            else if (result.StatusCode == 201)
                return Json(-1);
            return Json(result.Errors);
        }
        [Route("/Hakkimizda/{route}")]
        public ActionResult Page(string route)
        {
            var pageResult= _pageService.Where(x=>x.Route==route).Data.FirstOrDefault();
            PageDto pageDto = new PageDto { Id=pageResult.Id, Content=pageResult.Content, Title=pageResult.Title};
            return View(pageDto);
        }
    }
}