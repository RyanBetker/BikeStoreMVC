using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BikeStore;
using BikeStore.Models;
using BikeStore.ViewModels;

namespace BikeStore.Controllers.Admin
{
    public class BikeController : Controller
    {
        private BikeStoreContext db = new BikeStoreContext();

        // GET: Bike
        public ActionResult Index()
        {
            var bikesList = db.Bikes.ToList();
            var bikeViewModels = AutoMapper.Mapper.Map<IList<Bike>, IList<BikeViewModel>>(bikesList);
            
            return View(bikeViewModels);
        }

        // GET: Bike/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeViewModel bikeViewModel = AutoMapper.Mapper.Map<BikeViewModel>(db.Bikes.Find(id));
            if (bikeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeViewModel);
        }

        // GET: Bike/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bike/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BikeID,ModelNo,Type,FrameSize,WheelSize,Color,Brand,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] BikeViewModel bike)
        {
            if (ModelState.IsValid)
            {
                
                Bike bikeData = AutoMapper.Mapper.Map<Bike>(bike);

                AddBrandIfNotPresent(bike);
                db.Bikes.Add(bikeData);
                db.SaveChanges();
              
                return RedirectToAction("Index");
            }

            return View(bike);
        }

        private void AddBrandIfNotPresent(BikeViewModel bike)
        {
            if (db.Brands.Any(b => b.BrandName == bike.BrandName) == false)
            {
                db.Brands.Add(
                    new Brand()
                    {
                        BrandName = bike.BrandName,
                        CreatedBy = User.Identity.Name,
                        CreatedDate = DateTime.Now
                    });

            }
        }

        // GET: Bike/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeViewModel bikeViewModel = AutoMapper.Mapper.Map < BikeViewModel > (db.Bikes.Find(id));
            if (bikeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeViewModel);
        }

        // POST: Bike/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BikeID,ModelNo,Type,FrameSize,WheelSize,Color,BrandName,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate")] BikeViewModel bike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bike);
        }

        // GET: Bike/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeViewModel bikeViewModel = AutoMapper.Mapper.Map < BikeViewModel >(db.Bikes.Find(id));
            if (bikeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeViewModel);
        }

        // POST: Bike/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike bike= db.Bikes.Find(id);
            db.Bikes.Remove(bike);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
