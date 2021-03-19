using BusinessLayer.AppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    public class DealersController : Controller
    {
        DealerAppService dealerAppService = new DealerAppService();
        // GET: Admin/Dealers
        public ActionResult Index()
        {
            return View(dealerAppService.GetAllDealer());
        }
        

        public ActionResult Details(string id)
        {
            var employee = dealerAppService.GetDealer(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
    }
}