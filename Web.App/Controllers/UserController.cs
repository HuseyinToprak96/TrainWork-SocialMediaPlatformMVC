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
        public new async Task<ActionResult> Profile()
        {
            int userId =Convert.ToInt32(Session["UserId"]);
            var result=await _userService.GetAsync(userId);
            return View(result.Data);
        }
        public async Task<PartialViewResult> GetInfo()
        {
            int userId = 2;
            var result = await _userService.GetInfo(userId);
            return PartialView(result.Data);
        }

        public async Task<JsonResult> GetInfoUpdate(string json)
        {
            var data=JsonConvert.DeserializeObject<UserInfoUpdateDto>(json);
            var result= await _userService.UserInfoUpdate(data);
            if(result.Data)//Güncellendi ise
            return Json(1);
            return Json(0);
        }
    }
}