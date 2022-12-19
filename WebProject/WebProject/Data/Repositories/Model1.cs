using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebProject.Data.Repositories
{
    public partial class Model1 : DbContext
    {
        // lets make a connection string like this: Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\olgie\Desktop\New folder (2)\WebProject\WebApplication1\App_Data\Database1.mdf";Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework

        private static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\olgie\\Desktop\\New folder (2)\\WebProject\\WebApplication1\\App_Data\\Database1.mdf\";Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        public Model1()
        //: base("name=GasStationsContext") // THIS IS THE ORIGINAL LINE. IT WILL WORK IN ANY PC, BUT I MADE CHANGES THAT WILL WORK ONLY IN MY PC WITH DIRECT FULL PATH
        // pass connectionString
        : base(connectionString)
        {
        }


        public virtual DbSet<GasStation> GasStations { get; set; }
        public virtual DbSet<GasType> GasTypes { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Price> Prices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GasStation>()
                .Property(e => e.GasStationName)
                .IsUnicode(false);

            modelBuilder.Entity<GasStation>()
                .HasMany(e => e.Locations)
                .WithRequired(e => e.GasStation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<GasType>()
                .Property(e => e.GasTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<GasType>()
                .HasMany(e => e.Prices)
                .WithRequired(e => e.GasType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Location>()
                .Property(e => e.LocationName)
                .IsUnicode(false);

            modelBuilder.Entity<Location>()
                .HasMany(e => e.Prices)
                .WithRequired(e => e.Location)
                .WillCascadeOnDelete(false);
        }
    }
}
