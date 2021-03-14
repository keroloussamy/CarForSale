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
            ViewBag.CarType = "All Cars";
            return View(carAppService.GetAllCar());
        }

        public ActionResult New()
        {
            ViewBag.CarType = "New Cars";
            var cars = carAppService.GetAllCar().Where(c => c.Condition == Condition.New);
            return View("Index", cars.ToList());
        }
        public ActionResult Used()
        {
            ViewBag.CarType = "Used Cars";
            var cars = carAppService.GetAllCar().Where(c => c.Condition == Condition.Used);
            return View("Index", cars.ToList());
        }

        public PartialViewResult MessagePartial(string DealerID)
        {
            //ViewBag.DealerID = DealerID;

            MessageVM messageVM = new MessageVM { DealerId = DealerID }; 
            return PartialView("_MessagePartial", messageVM);
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
                    var senderEmail = new MailAddress("keroloussamy98@gmail.com", "Cars For Sale");
                    var receiverEmail = new MailAddress(emailVM.DealerEmail, "Receiver");
                    var password = "Put Your Password";
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
                    return RedirectToAction("Index");

                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Some Error";
            }
            return RedirectToAction("Contact");
        }
    }
}