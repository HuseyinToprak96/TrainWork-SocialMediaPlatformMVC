using CoreLayer.Dtos.Shared;
using CoreLayer.Dtos.User;
using CoreLayer.Entities.Notification;
using CoreLayer.Enum;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.App.Filters;
using Web.App.Framework;

namespace Web.App.Controllers
{
    [Authorize]
    [LoginFilter]
    [LogFilter]
    public class UserController : BaseController
    {
        
        public new async Task<ActionResult> Profile()
        {
            int userId =Convert.ToInt32(Session["UserId"]);
            var result=await _userService.GetAsync(userId);
            ViewBag.FollowerCount = _userService.GetFollowersCount(userId).Data;
            ViewBag.FollowingCount=_userService.GetFollowingsCount(userId).Data;
            return View(result.Data);
        }
        public async Task<PartialViewResult> GetInfo()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result = await _userService.GetInfo(userId);
            return PartialView(result.Data);
        }
        [HttpPost]
        public async Task<JsonResult> GetInfoUpdate(string json)
        {
            var data=JsonConvert.DeserializeObject<UserInfoUpdateDto>(json);
            int userId = Convert.ToInt32(Session["UserId"]);
            string[] uzantilar=new string[] { "jpg", "png" };
            string fileName = "";
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    //ImageSize imageSize = new ImageSize();
                    //imageSize.Execute(file);
                    int fileSize = file.ContentLength;
                    fileName =Guid.NewGuid()+file.FileName;
                    string uzanti = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    if (uzantilar.Any(x => x == uzanti))
                    {
                        data.ImagePath = fileName;
                        file.SaveAs(Server.MapPath("~/Content/images/") + fileName);
                    }
                }
            }
            data.UserId = userId;
            var result = await _userService.UserInfoUpdate(data);
            if(result.Data)//Güncellendi ise
            return Json(fileName);
            //result.Data ??= true; c# 8.0 ile geldi! null ise true yapar!
            return Json(0);
        }
        public async Task<PartialViewResult> GetBiography()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var user=await _userService.GetUserBiography(userId);
            return PartialView(user.Data);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateBiography(string json)
        {
            var userBiographyUpdateDto= JsonConvert.DeserializeObject<UserBiographyUpdateDto>(json);
            int userId = Convert.ToInt32(Session["UserId"]);
            userBiographyUpdateDto.UserId = userId;
            var user = await _userService.UserBiographyUpdate(userBiographyUpdateDto);
            if(user.Data)
            return Json(1);
            return Json(0);
        }
        public async Task<PartialViewResult> GetProfilePhotos()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var data=await _userService.GetUserPhotos(userId);
            return PartialView(data.Data);
        }

        public async Task<JsonResult> UpdateImage(int imageId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result=await _userService.UpdateProfilePhoto(userId, imageId);
            if(result.Data)
            return Json(1);
            return Json(result.Errors);
        }

        public async Task<PartialViewResult> GetShareds()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result=await _sharedService.GetUserShareds(userId);
            return PartialView(result.Data);
        }

        public async Task<ActionResult> AddShared(string json)
        {
            var sharedAddorUpdateDto=JsonConvert.DeserializeObject<SharedAddorUpdateDto>(json);
            int userId = Convert.ToInt32(Session["UserId"]);
            CoreLayer.Entities.Shared.Shared shared = new CoreLayer.Entities.Shared.Shared() { Title=sharedAddorUpdateDto.Title, Description=sharedAddorUpdateDto.Description, UserId=userId, CreatedUserId=userId, Type=EFileType.Text};
                
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file != null && file.ContentLength > 0)
                {
                    string[] uzantilarImages = new string[] { "jpg", "png" };
                    string[] uzantilarVideo = new string[] { "mp4" };
                    int fileSize = file.ContentLength;
                    string fileName = Guid.NewGuid() + file.FileName;
                    string uzanti = file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;
                    if (uzantilarImages.Any(x => x == uzanti))
                    {
                        shared.Path = fileName;
                        shared.Type=EFileType.Image;
                        file.SaveAs(Server.MapPath("~/Content/images/") + fileName);
                    }
                    else if (uzantilarVideo.Any(x => x == uzanti)){
                        shared.Path= fileName;
                        shared.Type=EFileType.Video;
                        file.SaveAs(Server.MapPath("~/Content/videos/") + fileName);
                    }
                }
            }
            await _sharedService.AddAsync(shared);
            shared= _sharedService.Where(x=>x.UserId==userId).Data.OrderByDescending(x=>x.Id).FirstOrDefault();
            var user=await _userService.GetAsync(userId);
            var followers = await _userService.GetIdUserFollowers(userId);
            List<Notification> notifications = new List<Notification>();
            followers.Data.ForEach(x =>
            {
                Notification notification = new Notification { Content = user.Data.Username+" adlı kullanıcının, "+shared.Title+" başlıklı gönderisini incele.", UserId=x, Link=$"{shared.Title}/#" , NotificationType=ENotificationType.Shared};
                notifications.Add(notification);
            });
            var result= await _notificationService.AddRangeAsync(notifications);
            return RedirectToAction("Profile");
        }

        public async Task<PartialViewResult> GetFollowers()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result = await _userService.GetFollowers(userId);
            var result2 = await _userService.GetFollowings(userId);
            return PartialView(Tuple.Create<IEnumerable<UserListDto>,IEnumerable<UserListDto>>(result.Data,result2.Data));
        }

        public async Task<JsonResult> RemoveFollowing(int followingId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result= await _userService.RemoveFollowerOrFollowingRemove(userId,followingId);
            if(result.StatusCode==200)
            return Json(1);
            return Json(result.Errors);
        }
        public async Task<JsonResult> RemoveFollower(int followerId)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result = await _userService.RemoveFollowerOrFollowingRemove(followerId, userId);
            if (result.StatusCode == 200)
                return Json(1);
            return Json(result.Errors);
        }

        public async Task<JsonResult> RemoveShared(int id)
        {
            var control = await _sharedService.AnyAsync(x => x.Id == id && x.IsActive && !x.IsDeleted);
            if (control.Data)
            {
                var shared = await _sharedService.GetAsync(id);
                shared.Data.IsDeleted = true;
                var result= await _sharedService.UpdateAsync(shared.Data);
                if (result.StatusCode == 200)
                {
                    return Json(1);
                }
            }
            return Json(0);
        }

        public async Task<PartialViewResult> GetShared(int id)
        {
            ViewBag.Id=id;
            var sharedResult = await _sharedService.GetAsync(id);
            return PartialView(sharedResult.Data);
        }

        public async Task<JsonResult> UpdateShared(string json)
        {
            var sharedAddorUpdateDto = JsonConvert.DeserializeObject<SharedAddorUpdateDto>(json);
            int userId = Convert.ToInt32(Session["UserId"]);
            var shared = await _sharedService.GetAsync(sharedAddorUpdateDto.SharedId);
            shared.Data.Title=sharedAddorUpdateDto.Title;
            shared.Data.Description=sharedAddorUpdateDto.Description;
            var updateResult = await _sharedService.UpdateAsync(shared.Data);
            if(updateResult.StatusCode==200)
            return Json(1);
            return Json(0);
        }
    }
}