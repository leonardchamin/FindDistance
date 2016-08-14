using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FindDistance.Data.Entities;


namespace FindDistance.Data
{
    public class FrogParkingDb: DbContext
    {
        public FrogParkingDb() : base("FrogParkingDbContext")
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
        }

        public DbSet<Route> Routes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
