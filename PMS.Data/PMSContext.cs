using Microsoft.AspNet.Identity.EntityFramework;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Data
{
    public class PMSContext : IdentityDbContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PMSContext>(null);
            base.OnModelCreating(modelBuilder);
        }
        public PMSContext() : base("PMSConnectionString")
        {
        }


        public static PMSContext Create()
        {
            return new PMSContext();
        }

        public DbSet<AccommodationType> AccommodationTypes { get; set; }
        public DbSet<AccommodationPackage> AccommodationPackages { get; set; }
        public DbSet<AccommodationPackagePicture> AccommodationPackagePictures { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<AccommodationPicture> AccommodationPictures { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Picture> Pictures { get; set; }
    }
}
