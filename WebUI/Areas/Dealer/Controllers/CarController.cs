using BusinessLayer.AppService;
using BusinessLayer.Bases;
using BusinessLayer.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Dealer.Controllers
{
    public class CarController : Controller
    {
        // GET: Dealer/Car
        CarAppService carAppService = new CarAppService();
        UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View(carAppService.GetAllCar().Where(c => c.DealerId == User.Identity.GetUserId()));
        }

        [HttpPost]
        public JsonResult CarsForBrand(int BrandId)
        {
            return Json(carAppService.GetAllCar().Where(c => c.BrandId == BrandId).Select(c => c.Model).ToList(), JsonRequestBehavior.AllowGet);
        }

        // GET: Dealer/Car/Details/5
        public ActionResult Details(int id)
        {
            var car = carAppService.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }




        // GET: Dealer/Car/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(unitOfWork.Brand.GetAll(), "ID", "Name");
            CarVM carVM = new CarVM();
            return View(carVM);
        }

        // POST: Dealer/Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarVM carVM, HttpPostedFileBase image)
        {
            try
            {
                var fileName = Path.GetFileName(image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                image.SaveAs(path);
                carVM.Image = fileName;
                carVM.DealerId = User.Identity.GetUserId();
                if (ModelState.IsValid)
                {
                    if (carAppService.SaveNewCar(carVM))
                        return RedirectToAction("Index");
                    else
                    {
                        ViewBag.BrandId = new SelectList(unitOfWork.Brand.GetAll(), "ID", "Name", carVM.BrandId);
                        return View(carVM);
                    }
                }
                else
                {
                    ViewBag.BrandId = new SelectList(unitOfWork.Brand.GetAll(), "ID", "Name", carVM.BrandId);
                    return View(carVM);
                }
            }
            catch (Exception)
            {
                ViewBag.BrandId = new SelectList(unitOfWork.Brand.GetAll(), "ID", "Name", carVM.BrandId);
                return View(carVM);
            }
        }







        // GET: Dealer/Car/Edit/5
        public ActionResult Edit(int id)
        {
            var car = carAppService.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
            return View(car);
        }

        // POST: Dealer/Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CarVM carVM, HttpPostedFileBase image)
        {//Delete the old Phot from the folder
            try
            {
                var fileName = Path.GetFileName(image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                image.SaveAs(path);
                carVM.Image = fileName;
                carVM.DealerId = User.Identity.GetUserId();
                if (ModelState.IsValid)
                {
                    if (carAppService.UpdateCar(carVM))
                        return RedirectToAction("Index");
                    else
                    {
                        //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
                        return View(carVM);
                    }
                }
                else
                {
                    //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
                    return View(carVM);
                }
            }
            catch (Exception)
            {
                //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
                return View(carVM);
            }

        }

        // GET: Dealer/Car/Delete/5
        public ActionResult Delete(int id)
        {
            var car = carAppService.GetCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
            return View(car);
        }

        // POST: Dealer/Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (carAppService.DeleteCar(id))
                return RedirectToAction("Index");
            else
            {
                var car = carAppService.GetCar(id);
                //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
                return View(car);
            }
        }
    }
}
