namespace BikeStore.Migrations
{
    using BikeStore.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BikeStore.BikeStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        internal void SeedFromExternal(BikeStore.BikeStoreContext context)
        {
            Seed(context);
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

            context.SaveChanges();

            context.Bikes.AddOrUpdate(b => b.ModelNo,
                new Bike() { Brand = context.Brands.First(b => b.BrandName == "Trek") , Color = "Green", CreatedBy = "Ryan", CreatedDate = DateTime.Now, ModelNo = "M800", FrameSize = FrameSizeType.Medium, Type = BikeType.Mountain, WheelSize = WheelSizeType.TwentyFive },
                new Bike() { Brand = context.Brands.First(b => b.BrandName == "Specialized"), Color = "Grey", CreatedBy = "Ryan", CreatedDate = DateTime.Now, ModelNo = "S100", FrameSize = FrameSizeType.Large, Type = BikeType.Hybrid, WheelSize = WheelSizeType.TwentySeven }
                );

            try
            {

                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationError in ex.EntityValidationErrors)
                {
                    Trace.WriteLine(String.Format("Error(s) with entity {0}", validationError.Entry.Entity.GetType().Name));

                    foreach (var error in validationError.ValidationErrors)
                    {
                        Trace.WriteLine(String.Format("{0}, bad value: {1}. Error Message: {2}", error.PropertyName,
                            validationError.Entry.CurrentValues.GetValue<object>(error.PropertyName),
                            error.ErrorMessage));
                                                
                    }
                }
                throw;
            }
        }
    }
}
