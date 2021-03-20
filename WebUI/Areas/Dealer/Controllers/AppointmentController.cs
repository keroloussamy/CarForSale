using BusinessLayer.AppService;
using BusinessLayer.Bases;
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
    
    public class AppointmentController : Controller
    {
        AppointmentAppService appointmentAppService = new AppointmentAppService();
        UnitOfWork unitOfWork = new UnitOfWork();


        [CustomAuthorize(Roles = "Dealer")]
        public ActionResult Index()
        {
            var DealerId = User.Identity.GetUserId();
            return View(appointmentAppService.GetAllAppointmentWhere(x => x.DealerId == DealerId));
        }


        [CustomAuthorize(Roles = "Dealer, Employee")]
        public ActionResult AppointmentsForEmployee()
        {
            var EmployeeId = User.Identity.GetUserId();
            return View("Index",appointmentAppService.GetAllAppointmentWhere(x => x.EmployeeId == EmployeeId));
        }

        [CustomAuthorize(Roles = "Dealer, Employee")]
        public ActionResult Details(int id)
        {
            var message = appointmentAppService.GetAppointment(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                if (appointmentAppService.SaveNewAppointment(appointment))
                    return RedirectToAction("Index", "Home", new { area = "" });
                else
                {
                    return View(appointment);
                }
            }
            else
            {
                return View(appointment);
            }

        }
        [CustomAuthorize(Roles = "Dealer")]
        public ActionResult Edit(int id)
        {
            var appointment = appointmentAppService.GetAppointment(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId =unitOfWork.Employee.GetWhere(x => x.DealerId == appointment.DealerId).Select(d => new SelectListItem{ Value = d.Id, Text = d.User.UserName });
            
            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Appointment appointment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (appointmentAppService.UpdateAppointment(appointment))
                        return RedirectToAction("Index");
                    else
                    {
                        ViewBag.EmployeeId = unitOfWork.Employee.GetWhere(x => x.DealerId == appointment.DealerId).Select(d => new SelectListItem { Value = d.Id, Text = d.User.UserName });
                        return View(appointment);
                    }
                }
                else
                {
                    ViewBag.EmployeeId = unitOfWork.Employee.GetWhere(x => x.DealerId == appointment.DealerId).Select(d => new SelectListItem { Value = d.Id, Text = d.User.UserName });
                    return View(appointment);
                }
            }
            catch (Exception)
            {
                ViewBag.EmployeeId = unitOfWork.Employee.GetWhere(x => x.DealerId == appointment.DealerId).Select(d => new SelectListItem { Value = d.Id, Text = d.User.UserName });
                return View(appointment);
            }

        }


        [CustomAuthorize(Roles = "Dealer")]
        public ActionResult Delete(int id)
        {
            if (appointmentAppService.DeleteAppointment(id))
                return RedirectToAction("Index");
            else
            {
                TempData["MyErrorMessage"] = "Some Thing Wrong";
                return RedirectToAction("Index");
            }
        }
    }
}