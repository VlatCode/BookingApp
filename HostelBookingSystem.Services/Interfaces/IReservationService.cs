using HostelBookingSystem.DTOs;
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
        void AddReservation(AddReservationDto room);
        void DeleteReservation(int id);
    }
}
