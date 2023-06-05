using Antlr.Runtime.Misc;
using CoreLayer.Dtos.User;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.App.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private UserService _userService=new UserService();
        // GET: User
        public ActionResult Login(string ReturnUrl="")
        {
            ViewData["url"] = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginDto loginDto,string returnUrl) 
        {
            var user=await _userService.Login(loginDto);
            if (user!=null)
            {
                FormsAuthentication.SetAuthCookie(user.Data.Id.ToString(),false);
                Session["UserId"]=user.Data.Id;

                return Redirect(returnUrl!=""?returnUrl:"/Home/Index");
            }
            return View();
        }

        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SignUp(CreateUserDto createUserDto)
        {
            var user=await _userService.AddUser(createUserDto);
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}