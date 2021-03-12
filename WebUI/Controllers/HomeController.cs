using BusinessLayer.AppService;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}