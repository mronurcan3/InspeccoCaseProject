using Project.BLL.DesignPatterns.GenericRepository.ConRep;
using Project.Common;
using Project.Entities.Enums;
using Project.Entities.Models;
using Project.MVCUI.Authentication;
using Project.MVCUI.ModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class AdminController : Controller
    {
        AppUserRepository _appUser;
        UserProfileRepository _userProfile;
        CompanyRepository _company;
        ContentRepository _content;
        public AdminController()
        {
            _appUser = new AppUserRepository();
            _userProfile = new UserProfileRepository(); 
            _company = new CompanyRepository();
            _content = new ContentRepository();

        }
        [AdminAuthentication]
        public ActionResult AdminAdd()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AdminAdd(string userName, string password, string firstName, string lastName, string email, string companyName,UserRole adminType)
        {

            if (_appUser.Any(x => x.UserName == userName))
            {
                ViewBag.AlreadyHave = "Username already taken";
                return View();
            }
            else if (_userProfile.Any(x => x.EMail == email))
            {
                ViewBag.AlreadyHave = "Email  already taken";
                return View();
            }


            AppUser adminAppUser = _appUser.Find(Convert.ToInt32(Session["admin"]));

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
                Role = adminType,
                UserProfile = userProfile,
                CompanyID = adminAppUser.CompanyID,
            };
            _appUser.Add(appUser);

            return View();
        }

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(string userName, string password)
        {
            

            if (_appUser.Any(x => x.UserName == userName && x.Password == password && x.Role == UserRole.Admin || x.Role == UserRole.ContentAdmin && x.Status != DataStatus.Deleted))
            {
                

                AppUser user = _appUser.FirstOrDefault(x => x.UserName == userName && x.Password == password);

                if(user.Role == UserRole.Admin)
                {
                    Session["admin"] = user.ID;

                    return RedirectToAction("AdminMainPage");
                }


                return RedirectToAction("ContentAdminPage");
            }

               

            return View();
        }
        [AdminAuthentication]
        public ActionResult AdminMainPage()
        {
            AppUser appUser = _appUser.Find(Convert.ToInt32(Session["admin"]));

            ContentVM CVM = new ContentVM()
            {
                Company  = _company.Find( Convert.ToInt32(appUser.CompanyID)),

                AppUser = appUser,
                Contents = _content.Where(x => x.CompanyID == appUser.CompanyID )
            };

            return View(CVM);
        }

        // GET: Admin
        public ActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult AdminRegister(string userName, string password, string firstName, string lastName, string email,string companyName)
        {
            if (_appUser.Any(x => x.UserName == userName))
            {
                ViewBag.ZatenVar = "Username already taken";
                return View();
            }
            else if (_userProfile.Any(x => x.EMail == email))
            {
                ViewBag.ZatenVar = "Email  already taken";
                return View();
            }

            else if (_company.Any(x => x.Name == companyName))
            {
                ViewBag.ZatenVar = "Company name already taken";
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

           
            Company company = new Company()
            {
                Name = companyName,
                AppUsers = new List<AppUser> { appUser }

            };

            _company.Add(company);

            string emailWillSend = "Congratulations, your account has been successfully created. To activate your account https://localhost:44312/Mail/AdminActivation/" + appUser.ActivationCode + " you can click the link";

            MailService.Send(appUser.UserProfile.EMail, body: emailWillSend, subject: "Acount Active!!!");

            TempData["EmailCheck"] = "please check your email";


            return RedirectToAction("EmailConfirm", "Mail");

        }

        [AdminAuthentication]
        public ActionResult AdminList()
        {
            AppUser appUser = _appUser.Find(Convert.ToInt32(Session["admin"]));
            ContentVM CVM = new ContentVM()
            {
                AppUser = appUser,
                AppUsers = _appUser.Where(x => x.CompanyID == appUser.CompanyID && x.Status != DataStatus.Deleted),
                Company = _company.Find(Convert.ToInt32(appUser.CompanyID))
                
            };
            return View(CVM);
        }

        [AdminAuthentication]

        public ActionResult AdminUpdate(int id)
        {
            ContentVM CVM = new ContentVM()
            {
                AppUser = _appUser.Find(id)
            };

            return View(CVM);
        }


        [HttpPost]

        public ActionResult AdminUpdate(AppUser appUser,UserRole adminType)
        {
            AppUser admin = _appUser.Find(Convert.ToInt32(Session["admin"]));
            appUser.Role = adminType;
            appUser.CompanyID = admin.CompanyID;
            appUser.UserProfile.ID = appUser.ID;
            _userProfile.Update(appUser.UserProfile);
            _appUser.Update(appUser);
           

            return RedirectToAction("AdminList");
        }


        [AdminAuthentication]
        public ActionResult AdminDelete(int id)
        {
            _userProfile.Delete(_userProfile.Find(id));
            _appUser.Delete(_appUser.Find(id));

            return RedirectToAction("AdminList");
        }

        [ContentAdminAuthentication]
        public ActionResult ContentAdminPage()
        {
            AppUser appUser = _appUser.Find(Convert.ToInt32(Session["contentAdmin"]));
            ContentVM CVM = new ContentVM()
            {
                Contents = appUser.Contents,

            };

            return View();
        }
    }
}