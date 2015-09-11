using BikeStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStore.Controllers.Admin
{
    [RoutePrefix("Admin/Brand")]
    //[Route("action=index")]
    public class BrandController : Controller
    {
        private BikeStoreCustomContext db = new BikeStoreCustomContext();

        // GET: Brand
        public ActionResult Index()
        {
            var brandList = db.Brands.ToList();
            var brandViewModels = AutoMapper.Mapper.Map<IList<BrandViewModel>>(brandList);

            return View(brandViewModels);
        }

        // GET: Brand/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        public ActionResult Create([Bind(Include="BrandName")] BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    brandViewModel.CreatedBy = this.GetUserName();
                    brandViewModel.CreatedDate = DateTime.Now;

                    var brand = AutoMapper.Mapper.Map<BikeStore.Models.Brand>(brandViewModel);
                    db.Brands.Add(brand);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    //TODO: Add Elmah error logging to log the ex.
                    ModelState.AddModelError("BrandID", "Error creating the brand.");
                    return View();
                }
            }
            return View();
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
            var brand = db.Brands.Find(id);
            var brandViewModel = AutoMapper.Mapper.Map<BrandViewModel>(brand);

            return View(brandViewModel);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BrandID, BrandName")] BrandViewModel brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Assign audit columns
                    brand.ModifiedBy = this.GetUserName();
                    brand.ModifiedDate = DateTime.Now;

                    var brandEntityToUpdate = db.Brands.First(b => b.BrandID == brand.BrandID);

                    //ensure these values get set to some values as they're Required and EF will freak if not given.
                    //I didn't allow them to be Bound because the user could have changed
                    brand.CreatedBy = brandEntityToUpdate.CreatedBy;
                    brand.CreatedDate = brandEntityToUpdate.CreatedDate;

                    brandEntityToUpdate = AutoMapper.Mapper.Map(brand, brandEntityToUpdate);

                    db.Entry(brandEntityToUpdate).State = EntityState.Modified;

                    //these audit columns' values shouldn't change at all or it wrecks the history data:
                    db.Entry(brandEntityToUpdate).Property(b => b.CreatedBy).IsModified = false;
                    db.Entry(brandEntityToUpdate).Property(b => b.CreatedDate).IsModified = false;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    //TODO: Add Elmah error logging to log the ex.
                    ModelState.AddModelError("BrandID", "Error updating the brand.");
                    return View();
                }
            }
            else
            {
                return View(brand);
            }
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var brand = db.Brands.Find(id);
                db.Brands.Remove(brand);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //TODO: Add Elmah error logging to log the ex.
                ModelState.AddModelError("BrandID", "Error deleting the brand.");
                return View();
            }
        }
    }
}
