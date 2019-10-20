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
        public PMSContext() : base("PMSConnectionString")
        {
        }


        public static PMSContext Create()
        {
            return new PMSContext();
        }

        public DbSet<AccommodationType> AccommodationTypes { get; set; }
        public DbSet<AccommodationPackage> accommodationPackages { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
