using BikeStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeStore.Extensions;

namespace BikeStore.Controllers
{
#if !DEBUG
    [Authorize] 
#endif
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
            var brandViewModel = FindBrandByID(id);
            if (brandViewModel != null)
            {
                return View(brandViewModel);                
            }
            else
            {
                return HttpNotFound();
            }
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
            return Add(brandViewModel);
        }

        [HttpPost]
        public ActionResult Add(BrandViewModel brandViewModel, bool fromClientPage = false)
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
                    if (fromClientPage)
                    {
                        return Json(brand.BrandID, JsonRequestBehavior.AllowGet); 
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch(DbUpdateException ex)
                {
                    //TODO: Add Elmah error logging to log the ex.
                    if (ex.IsUniqueConstraintViolation())
                    {
                        ModelState.AddModelError("", App_GlobalResources.ErrorMessages.BrandNameExists);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error creating the brand.");
                    }
                }
                catch (Exception ex)
                {
                    //TODO: Add Elmah error logging to log the ex.
                    ModelState.AddModelError("", "Error creating the brand.");
                    return View();
                }
            }
            return View();
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
            var brandViewModel = FindBrandByID(id);

            if (brandViewModel != null)
            {
                return View(brandViewModel); 
            }
            else
            {
                return HttpNotFound();
            }
        }

        private BrandViewModel FindBrandByID(int id)
        {
            var brand = db.Brands.Find(id);
            var brandViewModel = AutoMapper.Mapper.Map<BrandViewModel>(brand);
            return brandViewModel;
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
                catch (DbUpdateException ex)
                {
                    //TODO: Add Elmah error logging to log the ex.
                    if (ex.IsUniqueConstraintViolation())
                    {
                        ModelState.AddModelError("", App_GlobalResources.ErrorMessages.BrandNameExists);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error updating the brand.");
                    }
                    return View();
                }
                catch(Exception ex)
                {
                    //TODO: BUG: Needs unique constraint on brand and friendly message "BrandName already exists"

                    //TODO: Add Elmah error logging to log the ex.
                    ModelState.AddModelError("", "Error updating the brand.");
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
            var brandViewModel = FindBrandByID(id);

            if (brandViewModel != null)
            {
                return View(brandViewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var brand = db.Brands.Find(id);
                db.Brands.Remove(brand);
                //TODO: Should fail if any bikes are attached, but doesn't - wipes them. Would need an is active flag. instead
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
