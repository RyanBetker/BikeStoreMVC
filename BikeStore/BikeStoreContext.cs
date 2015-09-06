namespace BikeStore
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    public class BikeStoreCustomContext : BikeStoreContext
    {
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
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
    public class BikeStoreContext : DbContext
    {
        // Your context has been configured to use a 'Model1' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'BikeStore.Model1' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model1' 
        // connection string in the application configuration file.
        public BikeStoreContext()
            : base("name=BikeStoreContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public DbSet<BikeStore.Models.Bike> Bikes { get; set; }

        public DbSet<BikeStore.Models.Brand> Brands { get; set; }
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}