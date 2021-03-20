using BusinessLayer.AppService;
using BusinessLayer.ViewModels;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Accounts
{
    public class AccountController : Controller
    {
        AccountAppService accountAppService = new AccountAppService();
        DealerAppService dealerAppService = new DealerAppService();
        EmployeeAppService employeeAppService = new EmployeeAppService();
        // GET: Account
        
        public ActionResult Register()
        {
            ViewBag.RegisterType = "Become A Dealer";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM user)
        {
            if (ModelState.IsValid == false)
            {
                return View(user);
            }
            IdentityResult result = accountAppService.Register(user);
            if (result.Succeeded)
            {
                
                IAuthenticationManager owinMAnager = HttpContext.GetOwinContext().Authentication;
                //SignIn
                SignInManager<ApplicationUserIdentity, string> signinmanager =
                    new SignInManager<ApplicationUserIdentity, string>(
                        new ApplicationUserManager(), owinMAnager
                        );
                ApplicationUserIdentity identityUser = accountAppService.Find(user.UserName, user.PasswordHash);
                
                accountAppService.AssignToRole(identityUser.Id, "Dealer");  //-------------

                dealerAppService.SaveNewDealer(new DAL.Dealer { Id = identityUser.Id,  });   //To add UserId to Dealer table, like make the relatin manwal 

                signinmanager.SignIn(identityUser, true, true);
                return RedirectToAction("Index", "Car", new { area = "Dealer"});//Dealer area 
            }
            else
            {
                ModelState.AddModelError("", result.Errors.FirstOrDefault());
                return View(user);
            }
        }



        public ActionResult EmployeeRegister() {
            ViewBag.RegisterType = "Add New Employee";
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeRegister(RegisterVM user)
        {
            if (ModelState.IsValid == false)
            {
                return View(user);
            }
            IdentityResult result = accountAppService.Register(user);
            if (result.Succeeded)
            {
                ApplicationUserIdentity identityUser = accountAppService.Find(user.UserName, user.PasswordHash);

                accountAppService.AssignToRole(identityUser.Id, "Employee");  //-------------

                employeeAppService.SaveNewEmployee(new Employee { Id = identityUser.Id, DealerId = User.Identity.GetUserId() });   //To add UserId to Dealer table, like make the relatin manwal 

                return RedirectToAction("Index", "Employee", new { area = "Dealer"});//Dealer area
            }
            else
            {
                ModelState.AddModelError("", result.Errors.FirstOrDefault());
                return View(user);
            }
        }







        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(LoginVM user)
        {
            if (ModelState.IsValid == false)
            {
                return View(user);
            }
            ApplicationUserIdentity identityUser = accountAppService.Find(user.UserName, user.PasswordHash);

            if (identityUser != null)
            {
                IAuthenticationManager owinMAnager = HttpContext.GetOwinContext().Authentication;
                //SignIn
                SignInManager<ApplicationUserIdentity, string> signinmanager =
                    new SignInManager<ApplicationUserIdentity, string>(
                        new ApplicationUserManager(), owinMAnager
                        );
                signinmanager.SignIn(identityUser, true, true);
                return RedirectToAction("Index", "Home", new { area = ""});
            }
            else
            {
                ModelState.AddModelError("", "Not Valid Username & Password");
                return View(user);
            }

        }
        [Authorize]
        public ActionResult Logout()
        {
            IAuthenticationManager owinMAnager = HttpContext.GetOwinContext().Authentication;
            owinMAnager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index","Home", new { area = ""});
        }

    }
}