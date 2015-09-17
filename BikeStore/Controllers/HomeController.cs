using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStore.Controllers
{
    public class HomeController : Controller
    {
        private IBikeStoreRepository _db;
        public HomeController() : this(new BikeStoreCustomContext())
        {

        }
        public HomeController(IBikeStoreRepository db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            var db = new BikeStoreCustomContext();
            var brandListViewModels = AutoMapper.Mapper.Map<List<ViewModels.BrandViewModel>>(db.Brands);

            //return RedirectToAction("Index", "Bike");
            return View(brandListViewModels);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}