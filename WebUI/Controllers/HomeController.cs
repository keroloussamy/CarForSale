using BusinessLayer.AppService;
using BusinessLayer.ViewModels;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        CarAppService carAppService = new CarAppService();
        public ActionResult Index()
        {
            return View();
        }
        





        [HttpPost]
        public ActionResult AdvSearch(SearchCarVM searchCarVM)
        {
            var result = carAppService.GetAllCar();
            if (searchCarVM != null)
            {
                if (searchCarVM.Condition != null)
                    result = result.Where(x => x.Condition == searchCarVM.Condition).ToList();
                if (searchCarVM.Color != 0)
                    result = result.Where(x => x.Color == searchCarVM.Color).ToList();
                if (searchCarVM.MinPrice.HasValue)
                    result = result.Where(x => x.Price >= searchCarVM.MinPrice).ToList();
                if (searchCarVM.MaxPrice.HasValue)
                    result = result.Where(x => x.Price <= searchCarVM.MaxPrice).ToList();
                if (searchCarVM.Model != "null")//(!string.IsNullOrEmpty(searchCarVM.Model)) 
                    result = result.Where(x => x.Model.Contains(searchCarVM.Model)).ToList();
                if (searchCarVM.BrandId != null)
                    result = result.Where(x => x.BrandId == searchCarVM.BrandId).ToList();
            }
            if (result == null) //Need To review
            {
                ViewBag.Message = "No Matches Result";
            }
            return View("Cars", result);
        }

      
        public ActionResult ParaSearch(int MinPrice = 0, int MaxPrice = 0,string Condition = null)
        {
            var result = carAppService.GetAllCar();

            if (Condition != null && Condition != "All")
            {
                if (Condition == "Used")
                    result = result.Where(x => x.Condition == DAL.Condition.Used).ToList();
                else
                    result = result.Where(x => x.Condition == DAL.Condition.New).ToList();
            }
                if (MinPrice != 0)
                    result = result.Where(x => x.Price >= MinPrice).ToList();
                if (MaxPrice != 0)
                    result = result.Where(x => x.Price <= MaxPrice).ToList();

            return PartialView("_CarCardPartial", result);
        }








        public ActionResult Cars()
        {
            ViewBag.CarType = "All Cars";
            return View(carAppService.GetAllCar());
        }

        public ActionResult New()
        {
            ViewBag.CarType = "New Cars";
            var cars = carAppService.GetAllCar().Where(c => c.Condition == Condition.New);
            return View("Cars", cars.ToList());
        }
        public ActionResult Used()
        {
            ViewBag.CarType = "Used Cars";
            var cars = carAppService.GetAllCar().Where(c => c.Condition == Condition.Used);
            return View("Cars", cars.ToList());
        }












        public PartialViewResult MessagePartial(string DealerID)
        {
            MessageVM messageVM = new MessageVM { DealerId = DealerID }; 
            return PartialView("_MessagePartial", messageVM);
        }

        public PartialViewResult AppointmentPartial(string DealerID)
        {
            Appointment appointment = new Appointment { DealerId = DealerID };
            return PartialView("_AppointmentPartial", appointment);
        }

        public PartialViewResult EmailPartial(string DealerEmail)
        {

            EmailVM emailVM = new EmailVM { DealerEmail = DealerEmail }; 
            return PartialView("_EmailPartial", emailVM);
        }

        [HttpPost]
        public ActionResult SendEmail(EmailVM emailVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("Put Your Email", "Cars For Sale");//Put your Email, will send by it 
                    var receiverEmail = new MailAddress(emailVM.DealerEmail, "Receiver");
                    var password = "Put Your Password";               //Put your Password
                    var sub = emailVM.Subject;
                    var body = "Client Name : " + emailVM.Name + "<br>" +
                                "Client Email : " + emailVM.ClientEmail + "<br>" +
                                "Client Phone : " + emailVM.Phone + "<br>" +
                                "Client Message : " + emailVM.Body + "<br>";
                               
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        mess.IsBodyHtml = true;
                        smtp.Send(mess);
                    }
                    return RedirectToAction("Cars");

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("Cars");
        }
    }
}