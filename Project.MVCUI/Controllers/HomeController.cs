using Project.BLL.DesignPatterns.GenericRepository.ConRep;
using Project.Entities.Models;
using Project.MVCUI.ModelVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Controllers
{
    public class HomeController : Controller
    {
        ContentRepository _content;
        AppUserRepository _appUser;

        public HomeController()
        {
            _content = new ContentRepository();
            _appUser = new AppUserRepository(); 
        }
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                string ids = Session["user"].ToString();

                int id = Convert.ToInt32(ids);

                AppUser appUser = new AppUser();

                appUser = (_appUser.FirstOrDefault(x => x.ID == id));

                ContentVM CVM1 = new ContentVM()
                {
                    Contents = _content.GetActives(),
                    AppUser = appUser
                };



                return View(CVM1);
            }
            ContentVM CVM = new ContentVM()
            {
                Contents = _content.GetActives(),
            };           
            return View(CVM);        
        }

        [HttpPost]

        public ActionResult Index(int like)
        {
            Content content = _content.Find(like);

            content.Likes++;
            _content.Update(content);


            return RedirectToAction("Index");
        }
    }
}