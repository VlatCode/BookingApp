using HostelBookingSystem.DTOs;
using HostelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Mappers
{
    public static class ReservationMapper
    {
        public static ReservationDto ToReservationDto(this Reservation reservation)
        {
            return new ReservationDto
            {
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                RoomNumber = reservation.Room.Id // switch to new column RoomNumber (inside Room table)
            };
        }
    }
}
