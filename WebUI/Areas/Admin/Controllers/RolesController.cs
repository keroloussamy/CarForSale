using BusinessLayer.AppService;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        // GET: Admin/Role
        RoleAppService roleAppService = new RoleAppService();
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string RoleName)
        {
            if (RoleName != null)
            {
                
                IdentityResult result = roleAppService.Create(RoleName);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Dealers");
                }
                ViewBag.Error2 = result.Errors;
            }
            else
            {
                ViewBag.Error1 = "Role Can't Be Empty";
            }
            ViewBag.RoleName = RoleName;
            return View();
        }
    }
}