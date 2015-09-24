using BikeStore.ViewModels;
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
            InitializeMappings();

            SetupDatabaseIfNotExists();
        }

        private static void SetupDatabaseIfNotExists()
        {
            var context = new BikeStoreCustomContext();
            if (context.Database.CreateIfNotExists())
            {
                new BikeStore.Migrations.Configuration().SeedFromExternal(context);
            }
        }

        private void InitializeMappings()
        {
            InitializeBikeMappings();
        }

        private static void InitializeBikeMappings()
        {
            AutoMapper.Mapper.CreateMap<BikeViewModel, Models.Bike>();
            AutoMapper.Mapper.CreateMap<Models.Bike, BikeViewModel>()
                //if property mapping needed, example:
                .ForMember(vm => vm.BrandName, d => d.MapFrom(src => src.Brand.BrandName))
                .ForMember(vm => vm.ImageToShow, d => d.MapFrom(src => src.DisplayImage != null ? Convert.ToBase64String(src.DisplayImage) : ""));

            AutoMapper.Mapper.CreateMap<BrandViewModel, Models.Brand>().ReverseMap();
        }
    }
}
