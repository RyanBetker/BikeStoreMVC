using System;
namespace BikeStore
{
    public interface IBikeStoreRepository
    {
        System.Data.Entity.DbSet<BikeStore.Models.Bike> Bikes { get; set; }
        System.Data.Entity.DbSet<BikeStore.Models.Brand> Brands { get; set; }
        int SaveChanges();
    }
}
