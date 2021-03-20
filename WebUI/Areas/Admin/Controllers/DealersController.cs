using BusinessLayer.AppService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
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




        //Delete   ==>     Not implemented 


    }
}