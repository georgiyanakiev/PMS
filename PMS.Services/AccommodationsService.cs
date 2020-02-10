using PMS.Data;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
    public class AccommodationsService
    {
        public IEnumerable<Accommodation> GetAllAccommodations()
        {
            var context = new PMSContext();

            return context.Accommodations.ToList();
        }
        public IEnumerable<Accommodation> GetAllAccommodationsByAccommodationPackage(int accommodationPackageID)
        {
            var context = new PMSContext();

            return context.Accommodations.Where(x =>x.AccommodationPackageID == accommodationPackageID).ToList();
        }

        public IEnumerable<Accommodation> SearchAccommodations(string searchTerm, int? accommodationPackageID, int page, int recordSize)
        {
            var context = new PMSContext();

            var accommodations = context.Accommodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodations = accommodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accommodationPackageID.HasValue && accommodationPackageID.Value > 0)
            {
                accommodations = accommodations.Where(a => a.AccommodationPackageID == accommodationPackageID.Value);
            }

            var skip = (page - 1) * recordSize;

            return accommodations.OrderBy(x => x.AccommodationPackageID).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchAccommodationsCount(string searchTerm, int? accommodationPackageID)
        {
            var context = new PMSContext();

            var accommodations = context.Accommodations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                accommodations = accommodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (accommodationPackageID.HasValue && accommodationPackageID.Value > 0)
            {
                accommodations = accommodations.Where(a => a.AccommodationPackageID == accommodationPackageID.Value);
            }

            return accommodations.Count();
        }

        public Accommodation GetAccommodationByID(int ID)
        {
            using (var context = new PMSContext())
            {
                return context.Accommodations.Find(ID);
            }
        }

        public bool SaveAccommodation(Accommodation accommodation)
        {
            var context = new PMSContext();

            context.Accommodations.Add(accommodation);

            return context.SaveChanges() > 0;
        }

        public bool UpdateAccommodation(Accommodation accommodation)
        {
            var context = new PMSContext();

            context.Entry(accommodation).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteAccommodation(Accommodation accommodation)
        {
            var context = new PMSContext();

            context.Entry(accommodation).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}