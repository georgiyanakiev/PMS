using PMS.Data;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
    public class ReservationsService
    {
        public IEnumerable<Reservation> GetAllReservations()
        {
            var context = new PMSContext();

            return context.Reservations.ToList();
        }

        public IEnumerable<Reservation> GetAllReservationsByReservationPackage(int reservationPackageID)
        {
            var context = new PMSContext();

            return context.Reservations.Where(x =>x.ReservationID == reservationPackageID).ToList();
        }

        public IEnumerable<Reservation> SearchReservations(string searchTerm, int? reservationPackageID, int page, int recordSize)
        {
            var context = new PMSContext();

            var reservations = context.Reservations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reservations = reservations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (reservationPackageID.HasValue && reservationPackageID.Value > 0)
            {
                reservations = reservations.Where(a => a.ReservationID == reservationPackageID.Value);
            }

            var skip = (page - 1) * recordSize;

            return reservations.OrderBy(x => x.ReservationID).Skip(skip).Take(recordSize).ToList();
        }

        public int SearchReservationCount(string searchTerm, int? reservationPackageID)
        {
            var context = new PMSContext();

            var reservations = context.Reservations.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reservations = reservations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (reservationPackageID.HasValue && reservationPackageID.Value > 0)
            {
                reservations = reservations.Where(a => a.ReservationID == reservationPackageID.Value);
            }

            return reservations.Count();
        }

        public Reservation GetReservationByID(int ID)
        {
            using (var context = new PMSContext())
            {
                return context.Reservations.Find(ID);
            }
        }

        public bool SaveReservation(Reservation reservation)
        {
            var context = new PMSContext();

            context.Reservations.Add(reservation);

            return context.SaveChanges() > 0;
        }

        public bool UpdateReservation(Reservation reservation)
        {
            var context = new PMSContext();

            context.Entry(reservation).State = System.Data.Entity.EntityState.Modified;

            return context.SaveChanges() > 0;
        }

        public bool DeleteReservation(Reservation reservation)
        {
            var context = new PMSContext();

            context.Entry(reservation).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }
    }
}