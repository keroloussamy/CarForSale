using BusinessLayer.AppService;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Dealer.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeAppService employeeAppService = new EmployeeAppService();

        public ActionResult Index()
        {
            return View(employeeAppService.GetAllEmployee());
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