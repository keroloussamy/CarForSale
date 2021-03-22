using BusinessLayer.AppService;
using BusinessLayer.Bases;
using BusinessLayer.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;
using WebUI.MyHubs;

namespace WebUI.Areas.Dealer.Controllers
{
    [CustomAuthorize(Roles = "Dealer")]
    public class MessageController : Controller
    {
        // GET: Dealer/Message
        MessageAppService messageAppService = new MessageAppService();
        NotificationAppService notificationAppService = new NotificationAppService();
        public ActionResult Index()
        {
            notificationAppService.SetNotificationCountToZero(User.Identity.GetUserId());
            // Push Notify All Connect Client To set Count to zero
            IHubContext messageHubContext =
                GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
            messageHubContext.Clients.All.SetNotificationCountToZero(User.Identity.GetUserId());
            
            return View(messageAppService.GetAllMessage().Where(c => c.DealerId == User.Identity.GetUserId()));
        }


        //[ChildActionOnly]
        [HttpPost]
        public ActionResult IndexMessagesPartial()
        {
            return PartialView("_IndexMessagesPartial", messageAppService.GetAllMessage().Where(c => c.DealerId == User.Identity.GetUserId()));
        }


        // GET: Dealer/Message/Details/5
        public ActionResult Details(int id)
        {
            var message = messageAppService.GetMessage(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }




        // GET: Dealer/Message/Create
        //public ActionResult Create()
        //{
        //    MessageVM messageVM = new MessageVM();
        //    return View(messageVM);
        //}

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(MessageVM messageVM)
        {
            if (ModelState.IsValid)
            {
                if (messageAppService.SaveNewMessage(messageVM))
                {
                    // Push Notify All Connect Client to Increase Notification Count
                    IHubContext messageHubContext =
                        GlobalHost.ConnectionManager.GetHubContext<MessageHub>();
                    messageHubContext.Clients.All.NotifyNewMessage(messageVM.DealerId);

                    notificationAppService.IncrementNotificationCount(messageVM.DealerId);

                    return RedirectToAction("Index", "Home", new { area = "" });
                }
                else
                {
                    return View(messageVM);
                }
            }
            else
            {
                return View(messageVM);
            }
            
        }






/*
        // GET: Dealer/Message/Edit/5
        public ActionResult Edit(int id)
        {
            var message = messageAppService.GetMessage(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Dealer/Message/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MessageVM messageVM)
        {
            //messageVM.DealerId   ==> note
            if (ModelState.IsValid)
            {
                if (messageAppService.UpdateMessage(messageVM))
                    return RedirectToAction("Index");
                else
                {
                    return View(messageVM);
                }
            }
            else
            {
                return View(messageVM);
            }
        }
*/




        // GET: Dealer/Message/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    var message = messageAppService.GetMessage(id);
        //    if (message == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(message);
        //}

        // POST: Dealer/Message/Delete/5


        public JsonResult Delete(int id)
        {
            if (messageAppService.DeleteMessage(id))
            {
                return Json(new { status = "Success" });
            }
            else
            {
                return Json(new { status = "Faild" });
            }
        }
    }
}