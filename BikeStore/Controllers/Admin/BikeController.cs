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
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Controllers.Admin
{
    [RoutePrefix("Admin/Bike")]
    [Route("{action=index}")]
    public class BikeController : Controller
    {
        private BikeStoreContext db = new BikeStoreContext();

        // GET: Bike
        public ActionResult Index()
        {
            var bikesList = db.Bikes.Include(b => b.Brand).ToList();
            var destination = new List<BikeViewModel>();
            for (int i = 0; i < bikesList.Count; i++)
            {
                destination.Add(new BikeViewModel());
            }
            
            //wasn't able to get these going, so property for AvailableBrands needs to be public
            //Func<BikeViewModel, object> funcConstruction = type => { return new BikeViewModel(); };
            //, opt => opt.ConstructServicesUsing(funcConstruction));

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
            BikeViewModel bikeViewModel = FindBikeByID(id);
            if (bikeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeViewModel);
        }

        // GET: Bike/Create
        public ActionResult Create()
        {
            var newBike = new BikeViewModel();
            newBike.Brands = AutoMapper.Mapper.Map<List<BrandViewModel>>(db.Brands);
            return View(newBike);
        }

        // POST: Bike/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BikeID,ModelNo,WholesalePrice,Price,Type,FrameSize,WheelSize,Color,Brands,BrandID")] BikeViewModel bike)
        {
            bike.CreatedBy = this.GetUserName();
            bike.CreatedDate = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                Bike bikeData = AutoMapper.Mapper.Map<Bike>(bike);

                db.Bikes.Add(bikeData);
                //TODO: BUG: Needs unique constraint on brandID, ModelNo and friendly message "Model already exists for this brand"
                db.SaveChanges();
              
                return RedirectToAction("Index");
            }

            return View(bike);
        }

        private Brand AddBrandIfNotPresent(BikeViewModel bike)
        {
            var brand = db.Brands.FirstOrDefault(b => b.BrandName == bike.BrandName);

            if (brand == null && String.IsNullOrWhiteSpace(bike.BrandName) == false)
            {
                brand =
                    new Brand()
                    {
                        BrandName = bike.BrandName,
                        CreatedBy = this.GetUserName(),
                        CreatedDate = DateTime.Now
                    };

                db.Brands.Add(brand);
                db.SaveChanges();

                return brand; 
            }

            return brand;
        }

        // GET: Bike/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeViewModel bikeViewModel = FindBikeByID(id, includeBrandList: true);
            if (bikeViewModel == null)
            {
                return HttpNotFound();
            }

            bikeViewModel.Brands = AutoMapper.Mapper.Map<List<BrandViewModel>>(db.Brands);

            return View(bikeViewModel);
        }

        // POST: Bike/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BikeID,ModelNo,WholesalePrice,Price,Type,FrameSize,WheelSize,Color,BrandID,Brands")] BikeViewModel bike)
        {
            if (ModelState.IsValid)
            {
                //Assign audit columns
                bike.ModifiedBy = this.GetUserName();
                bike.ModifiedDate = DateTime.Now;

                var bikeEntityToUpdate = db.Bikes.First(b => b.BikeID == bike.BikeID);

                //ensure these values get set to some values as they're Required and EF will freak if not given.
                //I didn't allow them to be Bound because the user could have changed
                bike.CreatedBy = bikeEntityToUpdate.CreatedBy;
                bike.CreatedDate = bikeEntityToUpdate.CreatedDate;

                bikeEntityToUpdate = AutoMapper.Mapper.Map(bike, bikeEntityToUpdate);
                db.Entry(bikeEntityToUpdate).State = EntityState.Modified;
                
                //these audit columns' values shouldn't change at all or it wrecks the history data:
                bool createdByIsModified = db.Entry(bikeEntityToUpdate).Property(b => b.CreatedBy).IsModified;
                
                db.Entry(bikeEntityToUpdate).Property(b => b.CreatedBy).IsModified = false;
                db.Entry(bikeEntityToUpdate).Property(b => b.CreatedDate).IsModified = false;

                //TODO: BUG: Needs unique constraint on brandID, ModelNo and friendly message "Model already exists for this brand"
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
            BikeViewModel bikeViewModel = FindBikeByID(id);
            if (bikeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(bikeViewModel);
        }

        private BikeViewModel FindBikeByID(int? id, bool includeBrandList = false)
        {
            BikeViewModel bikeViewModel;

            if (includeBrandList)
            {
                bikeViewModel = new BikeViewModel();//db.Brands.ToList());
            }
            else
            {
                bikeViewModel = new BikeViewModel();
            }

            var bikeData = db.Bikes.Include(b => b.Brand).FirstOrDefault(b => b.BikeID == id);

            bikeViewModel = AutoMapper.Mapper.Map<BikeViewModel>(bikeData, 
                opt => opt.ConstructServicesUsing(f => bikeViewModel));
            
            return bikeViewModel;
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
