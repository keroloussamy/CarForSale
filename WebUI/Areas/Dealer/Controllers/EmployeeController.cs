using BusinessLayer.AppService;
using DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Areas.Dealer.Controllers
{
    [CustomAuthorize(Roles = "Dealer")]
    public class EmployeeController : Controller
    {
        EmployeeAppService employeeAppService = new EmployeeAppService();

        public ActionResult Index()
        {
            var DealerId = User.Identity.GetUserId();
            return View(employeeAppService.GetAllEmployeeWhere(x => x.DealerId == DealerId));
        }

        public ActionResult Details(string id)
        {
            var employee = employeeAppService.GetEmployee(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        public ActionResult Edit(string id)
        {
            var employee = employeeAppService.GetEmployeeAsUSer(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUserIdentity employee)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (employeeAppService.UpdateEmployee(employee))
                        return RedirectToAction("Index");
                    else
                    {
                        return View(employee);
                    }
                }
                else
                {
                    return View(employee);
                }
            }
            catch (Exception)
            {
                return View(employee);
            }

        }


        //public ActionResult Delete(string id)
        //{
        //    var employee = employeeAppService.GetEmployee(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(employee);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    if (employeeAppService.DeleteEmployee(id))
        //        return RedirectToAction("Index");
        //    else
        //    {
        //        var employee = employeeAppService.GetEmployee(id);

        //        return View(employee);
        //    }
        //}

    }
}