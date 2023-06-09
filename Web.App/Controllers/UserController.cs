using CoreLayer.Dtos.User;
using Newtonsoft.Json;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.App.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserService _userService=new UserService();
        private SharedService _sharedService=new SharedService();
        public new async Task<ActionResult> Profile()
        {
            int userId =Convert.ToInt32(Session["UserId"]);
            var result=await _userService.GetAsync(userId);
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

        public async Task<PartialViewResult> GetShareds()
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            var result=await _sharedService.GetUserShareds(userId);
            return PartialView(result.Data);
        }
    }
}