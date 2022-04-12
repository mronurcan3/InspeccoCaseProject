using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.MVCUI.Authentication
{
    public class ContentAdminAuthentication: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["contentAdmin"] != null)
            {
                return true;
            }
            httpContext.Response.Redirect("/Admin/AdminLogin");
            return false;
        }
    }
}