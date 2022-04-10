using Project.BLL.DesignPatterns.GenericRepository.ConRep;
using Project.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        AppUserRepository _appUser;
        public MailController()
        {
            _appUser = new AppUserRepository();
        }
        public ActionResult Activation(Guid id)
        { //mail activasyonu
            AppUser willactive = _appUser.FirstOrDefault(x => x.ActivationCode == id);
            if (willactive != null)
            {
                willactive.Role = Entities.Enums.UserRole.Member;
                _appUser.Update(willactive);
                TempData["isAccountActive"] = "Your e-mail has been successfully confirmed";
                return RedirectToAction("EmailConfirm", "Mail");
            }
            TempData["isAccountActive"] = "Could not found any account";
            return RedirectToAction("EmailConfirm", "Mail");
        }

        public ActionResult EmailConfirm()
        {  //email dogrulama bilgisi icin
            return View();

        }
    }
}