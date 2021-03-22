using BusinessLayer.AppService;
using DAL;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Accounts.Controllers
{
    public class NotificationController : Controller
    {
        NotificationAppService notificationAppService = new NotificationAppService();
        // GET: Accounts/Notification
        [ChildActionOnly]
        public ActionResult Alerts()
        {
            var notif = notificationAppService.GetNotification(User.Identity.GetUserId());
            return PartialView(notif);
        }

    }
}