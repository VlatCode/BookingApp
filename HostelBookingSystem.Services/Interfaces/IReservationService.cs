using HostelBookingSystem.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Services.Interfaces
{
    public interface IReservationService
    {
        List<ReservationDto> GetAllReservations();
        ReservationDto GetById(int id);
        void AddReservation(AddReservationDto reservation);
        void DeleteReservation(int id);
        void UpdateReservation(UpdateReservationDto reservation);
    }
}
