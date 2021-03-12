using BusinessLayer.AppService;
using BusinessLayer.Bases;
using BusinessLayer.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Dealer.Controllers
{
    public class MessageController : Controller
    {
        // GET: Dealer/Message
        MessageAppService messageAppService = new MessageAppService();
        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(messageAppService.GetAllMessage().Where(c => c.DealerId == User.Identity.GetUserId()));
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
        public ActionResult Create()
        {
            MessageVM messageVM = new MessageVM();
            return View(messageVM);
        }

        // POST: Dealer/Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MessageVM messageVM)
        {
            //messageVM.DealerId ==> shold be get from the car
            if (ModelState.IsValid)
            {
                if (messageAppService.SaveNewMessage(messageVM))
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





        // GET: Dealer/Message/Delete/5
        public ActionResult Delete(int id)
        {
            var message = messageAppService.GetMessage(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Dealer/Message/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (messageAppService.DeleteMessage(id))
                return RedirectToAction("Index");
            else
            {
                var message = messageAppService.GetMessage(id);
                return View(message);
            }
        }
    }
}