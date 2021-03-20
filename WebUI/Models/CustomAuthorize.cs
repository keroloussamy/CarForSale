using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Models
{
   
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //you can change to any controller or html page.
            filterContext.Result = new RedirectResult("/Accounts/Account/Login");

        }
    }

}