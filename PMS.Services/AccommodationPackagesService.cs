using PMS.Data;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
    public class AccommodationPackagesService
    {
        public IEnumerable<AccommodationPackage> GetAllAccommodationPackages()
        {
            var context = new PMSContext();

            return context.AccommodationPackages.ToList();
        }
        public IEnumerable<AccommodationPackage> SearchAccommodationPackages(string searchTerm)
        {
            var context = new PMSContext();

            var accommodationPackages = context.AccommodationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodationPackages = accommodationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            
            return context.AccommodationPackages.ToList();
        }
        public AccommodationPackage GetAccommodationPackageByID(int ID)
        {   
            using (var context = new PMSContext())
            {
                return context.AccommodationPackages.Find(ID);
            }

            
        }
        public bool SaveAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            var context = new PMSContext();

            context.AccommodationPackages.Add(accommodationPackage);

            return context.SaveChanges() > 0;
        }
        public bool UpdateAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            var context = new PMSContext();

            context.Entry(accommodationPackage).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
        public bool DeleteAccommodationPackage(AccommodationPackage accommodationPackage)
        {
            var context = new PMSContext();

            context.Entry(accommodationPackage).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}
