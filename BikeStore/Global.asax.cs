using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace BikeStore
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            using (var context = new BikeStoreContext())
            {
                if (context.Brands.Any() == false)
                {
                    context.Brands.Add(new Models.Brand() { BrandName = "Trek", CreatedBy = "Ryan Betker", CreatedDate = DateTime.Now });
                    context.SaveChanges(); 
                }
            }
        }
    }
}
