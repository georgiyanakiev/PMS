using PMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMS.ViewModels
{
    public class ReservationsViewModels
    {   
        public ReservationType ReservationType { get; set; }
        public IEnumerable<Reservation> Reservations { get; set; }
        public string SearchTerm { get; set; }
        public int ReservationPackageID { get; set; }
        public IEnumerable<ReservationPackage> ReservationPackages { get; set; }
        public Pager Pager { get; set; }
        public int SelectedReservationPackageID { get; set; }
    }
    public class ReservationPackageDetailsViewModel
    {
        public ReservationPackage ReservationPackages { get; set; }
    }

    public class CheckReservationAvailabilityViewModel
    {
        public Reservation ReservationsService { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int Duration { get; set; }
        public int NoOfAdults { get; set; }
        public int ReservationNumber { get; set; }
        public int NoOfChildren { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
    }
}