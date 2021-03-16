using BusinessLayer.AppService;
using BusinessLayer.Bases;
using BusinessLayer.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Admin
{
    public class BrandController : Controller
    {
        BrandAppService brandAppService = new BrandAppService();
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Brand
        public ActionResult Index()
        {
            return View(brandAppService.GetBrands());
        }

        public JsonResult GetBrands()
        {
            return Json(brandAppService.GetBrands(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var brand = brandAppService.GetBrand(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        public ActionResult Create()
        {
            BrandVM brandVM = new BrandVM();
            return View(brandVM);
        }

        // POST: Dealer/Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BrandVM brandVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (brandAppService.insertNewBrand(brandVM))
                        return RedirectToAction("Index");
                    else
                    {
                        return View(brandVM);
                    }
                }
                else
                {
                    return View(brandVM);
                }


            }
            catch (Exception)
            {
                return View(brandVM);
            }
        }

        // GET: Dealer/Car/Edit/5
        public ActionResult Edit(int id)
        {
            var brand = brandAppService.GetBrand(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Dealer/Car/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BrandVM brandVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (brandAppService.UpdateBrand(brandVM))
                        return RedirectToAction("Index");
                    else
                    {
                        return View(brandVM);
                    }
                }
                else
                {
                    return View(brandVM);
                }
            }
            catch (Exception)
            {
                return View(brandVM);
            }

        }


        // GET: Dealer/Car/Delete/5
        public ActionResult Delete(int id)
        {
            var brand = brandAppService.GetBrand(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
            return View(brand);
        }

        // POST: Dealer/Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (brandAppService.DeleteBrand(id))
                return RedirectToAction("Index");
            else
            {
                var brand = brandAppService.GetBrand(id);
                //ViewBag.BrandId = new SelectList(unitOfWork.Brands, "ID", "Name", carVM.BrandId);
                return View(brand);
            }
        }


    }
}