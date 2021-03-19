﻿using BusinessLayer.AppService;
using BusinessLayer.Bases;
using DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Dealer.Controllers
{
    public class AppointmentController : Controller
    {
        AppointmentAppService appointmentAppService = new AppointmentAppService();
        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(appointmentAppService.GetAllAppointmentWhere(User.Identity.GetUserId()));
        }

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

        public ActionResult Edit(int id)
        {
            var appointment = appointmentAppService.GetAppointment(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            //IEnumerable<SelectListItem> items = 
            ViewBag.EmployeeId =unitOfWork.Employee.GetWhere(x => x.DealerId == appointment.DealerId).Select(d => new SelectListItem{ Value = d.Id, Text = d.User.UserName });
            //new SelectList(unitOfWork.Employee.GetWhere(x => x.DealerId == appointment.DealerId), "ID", "Name");
//            IEnumerable<SelectListItem> items = unitOfWork.Employee.GetWhere(x => x.DealerId == appointment.DealerId)
//.Select(d => new SelectListItem { Value = d.Id, Text = d.User.UserName });
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
    }
}