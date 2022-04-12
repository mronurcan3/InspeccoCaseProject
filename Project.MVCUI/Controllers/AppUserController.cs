using Project.BLL.DesignPatterns.GenericRepository.ConRep;
using Project.Common;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class AppUserController : Controller
    {

        AppUserRepository _appUser;
        UserProfileRepository _userProfile;

        public AppUserController()
        {
            _appUser = new AppUserRepository();
            _userProfile = new UserProfileRepository();
        }
        // GET: AppUser

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            if (_appUser.Any(x => x.UserName == userName && x.Password == password))
            {

                AppUser user = _appUser.FirstOrDefault(x => x.UserName == userName && x.Password == password);

                Session["user"] = user.ID;
            }



            return RedirectToAction("Index","Home");
            
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string userName, string password, string firstName, string lastName, string email)
        {
            if (_appUser.Any(x => x.UserName == userName))
            {
                ViewBag.ZatenVar = "Username already taken";
                return View();
            }
            else if (_userProfile.Any(x => x.EMail == email))
            {
                ViewBag.ZatenVar = "Email already taken";
                return View();
            }
            

            UserProfile userProfile = new UserProfile()
            {
                FirstName = firstName,
                LastName = lastName,
                EMail = email,              
            };
            AppUser appUser = new AppUser()
            {
                UserName = userName,
                Password = password,
                Role = Entities.Enums.UserRole.Visitor,
                UserProfile = userProfile,
            };
            _appUser.Add(appUser);

            string emailWillSend = "Congratulations, your account has been successfully created. To activate your account https://localhost:44312/Mail/Activation/" + appUser.ActivationCode + " you can click the link";

            MailService.Send(appUser.UserProfile.EMail, body: emailWillSend, subject: "Acount Actiobation!!!");

            TempData["EmailCheck"] = "please check your email";

            return RedirectToAction("EmailConfirm", "Mail");

        }

        public ActionResult Logout()
        {

            Session.Remove("user");

           return RedirectToAction("Index", "Home");
        }

        
    }
}