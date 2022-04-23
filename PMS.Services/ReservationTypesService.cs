using PMS.Data;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
    public class ReservationTypesService
    {
        public IEnumerable<ReservationType> GetAllReservationTypes()
        {
            var context = new PMSContext();

            return context.ReservationTypes.ToList();
        }

        public IEnumerable<ReservationType> SearchReservationTypes(string searchTerm)
        {
            var context = new PMSContext();

            var reservationTypes = context.ReservationTypes.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reservationTypes = reservationTypes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return reservationTypes.ToList();
        }

        public ReservationType GetReservationTypeByID(int? ID)
        {
            var context = new PMSContext();

            return context.ReservationTypes.Find(ID);
        }

        public bool SaveReservationType(ReservationType reservationType)
        {
            var context = new PMSContext();

            context.ReservationTypes.Add(reservationType);

            return context.SaveChanges() > 0;
        }

        public bool UpdateReservationType(ReservationType reservationType)
        {
            var context = new PMSContext();

            context.Entry(reservationType).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteReservationType(ReservationType reservationType)
        {
            var context = new PMSContext();

            context.Entry(reservationType).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}