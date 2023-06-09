using Antlr.Runtime.Misc;
using CoreLayer.Dtos.Mail;
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
        private MailService _mailService=new MailService(); 
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
            if (user.Data!=null)
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
        public ActionResult Forgot(int error=0)
        {
            ViewBag.Error = error==1?"Önce üye olmanız gerekir!":"";
            return View();
        }
        [HttpPost]
        public  ActionResult Forgot(string email)
        {
            var response = _userService.Where(x => x.Email == email);
            if (response.Data.Count()>0)
            {
                var user = response.Data.FirstOrDefault();
            MailDto mailDto = new MailDto { Body = $"Şifrenizi unuttuysanız yeni şifre oluşturmak için <a href='http://localhost:44379/Auth/PasswordUpdate?u={user.Id}'> tıklayınız.</a>", Contact=email, Subject="Şifremi Unuttum" };
            bool IsSend = _mailService.SendMail(mailDto);
            }
            return Redirect("/Auth/Forgot?error=1");
        }
        public ActionResult PasswordUpdate(int u)
        {
            ViewBag.Id = u;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> PasswordUpdate(string newPassword,int userId)
        {
            var result = await _userService.UpdatePassword(newPassword,userId);
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