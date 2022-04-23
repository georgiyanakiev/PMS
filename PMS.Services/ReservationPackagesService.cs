using PMS.Data;
using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Services
{
    public class ReservationPackagesService
    {
        
        public IEnumerable<ReservationPackage> GetAllReservationPackages()
        {
            var context = new PMSContext();

            return context.ReservationPackages.ToList();
        }
        public IEnumerable<ReservationPackage> GetAllReservationPackagesByReservationType(int? reservationTypeID)
        {
            var context = new PMSContext();

            return context.ReservationPackages.Where(x=>x.ReservationTypeID == reservationTypeID).ToList();
        }
        public IEnumerable<ReservationPackage> SearchReservationPackagesCount(string searchTerm, int? reservationTypeID, int page, int recordSize)
        {
            var context = new PMSContext();

            var reservationPackages = context.ReservationPackages.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                reservationPackages = reservationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (reservationTypeID.HasValue && reservationTypeID.Value > 0)
            {
                reservationPackages = reservationPackages.Where(a => a.ReservationTypeID == reservationTypeID.Value);
            }

            var skip = (page - 1) * recordSize;
            //  skip = (1    -  1) = 0 * 3 = 0
            //  skip = (2    -  1) = 1 * 3 = 3
            //  skip = (3    -  1) = 2 * 3 = 6

            return reservationPackages.OrderBy(x => x.ReservationTypeID).Skip(skip).Take(recordSize).ToList();
        }
         public int SearchReservationPackagesCount(string searchTerm, int? reservationTypeID)
        {
            var context = new PMSContext();

            var reservationPackages = context.ReservationPackages.AsQueryable();

            if  (!string.IsNullOrEmpty(searchTerm))
            {
                reservationPackages = reservationPackages.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if  (reservationTypeID.HasValue && reservationTypeID.Value > 0)
            {
                reservationPackages = reservationPackages.Where(a => a.ReservationTypeID == reservationTypeID.Value);
            }

           

            return context.ReservationPackages.Count();
        }
        public ReservationPackage GetReservationPackageByID(int ID)
        {
            var context = new PMSContext();

            return context.ReservationPackages.Find(ID);
        }
        public bool SaveReservationPackage(ReservationPackage reservationPackage)
        {
            var context = new PMSContext();

            context.ReservationPackages.Add(reservationPackage);

            return context.SaveChanges() > 0;
        }
        public bool UpdateReservationPackage(ReservationPackage reservationPackage)
        {
            var context = new PMSContext();

            var existingReservationPackage = context.ReservationPackages.Find(reservationPackage.ID);

            context.ReservationPackagePictures.RemoveRange(existingReservationPackage.ReservationPackagePicture);

            context.Entry(existingReservationPackage).CurrentValues.SetValues(reservationPackage);

            context.ReservationPackagePictures.AddRange(reservationPackage.ReservationPackagePicture);

            return context.SaveChanges() > 0;
        }
        public bool DeleteReservationPackage(ReservationPackage reservationPackage)
        {
            var context = new PMSContext();


            var existingReservationPackage = context.ReservationPackages.Find(reservationPackage.ID);

            context.ReservationPackagePictures.RemoveRange(existingReservationPackage.ReservationPackagePicture);
            context.Entry(existingReservationPackage).State = System.Data.Entity.EntityState.Deleted;

            return context.SaveChanges() > 0;
        }

        public List<ReservationPackagePicture> GetPicturesByReservationPackageID(int reservationPackageID)
        {
            var context = new PMSContext();

            return context.ReservationPackages.Find(reservationPackageID).ReservationPackagePicture.ToList();
        }
    }
}
