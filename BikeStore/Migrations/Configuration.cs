namespace BikeStore.Migrations
{
    using BikeStore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BikeStore.BikeStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BikeStore.BikeStoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Brands.AddOrUpdate(b => b.BrandName,
                new Brand() { BrandName = "Trek", CreatedBy = "Ryan", CreatedDate = DateTime.Now },
                new Brand() { BrandName = "Surly", CreatedBy = "Ryan", CreatedDate = DateTime.Now.AddDays(-2) },
                new Brand() { BrandName = "Specialized", CreatedBy = "Joe Admin", CreatedDate = DateTime.Now.Date.AddMonths(-1) }
                );

            context.Bikes.AddOrUpdate(b => b.ModelNo,
                new Bike() { Brand = new Brand() { BrandName = "Trek" }, Color = "Green", CreatedBy = "Ryan", CreatedDate = DateTime.Now, ModelNo = "M800", FrameSize = FrameSizeType.Medium, Type = BikeType.Mountain, WheelSize = WheelSizeType.TwentyFive },
                new Bike() { Brand = new Brand() { BrandName = "Specialized"}, Color = "Grey", CreatedBy = "Ryan", CreatedDate = DateTime.Now, ModelNo = "S100", FrameSize = FrameSizeType.Large, Type = BikeType.Hybrid, WheelSize = WheelSizeType.TwentySeven });

            context.SaveChanges();
        }
    }
}
