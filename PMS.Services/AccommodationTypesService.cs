using PMS.Data;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
    public class AccommodationTypesService
    {
        public IEnumerable<AccommodationType> GetAllAccommodationTypes()
        {
            var context = new PMSContext();

            return context.AccommodationTypes.ToList();
        }

        public IEnumerable<AccommodationType> SearchAccommodationTypes(string SearchTerm)
        {
            var context = new PMSContext();

            var accommodationTypes = context.AccommodationTypes.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                accommodationTypes = accommodationTypes.Where(a => a.Name.ToLower().Contains(SearchTerm.ToLower()));
            }

            return accommodationTypes.ToList();
        }
        public AccommodationType GetAccommodationTypeByID(int ID)
        {
            var context = new PMSContext();

            return context.AccommodationTypes.Find(ID);
        }
        public bool SaveAccommodationType(AccommodationType accommodationType)
        {
            var context = new PMSContext();

            context.AccommodationTypes.Add(accommodationType);

            return context.SaveChanges() > 0;
        }
        public bool UpdateAccommodationType(AccommodationType accommodationType)
        {
            var context = new PMSContext();

            context.Entry(accommodationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }
        public bool DeleteAccommodationType(AccommodationType accommodationType)
        {
            var context = new PMSContext();

            context.Entry(accommodationType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}