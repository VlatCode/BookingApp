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
                RoomId = reservation.Room.Id,
            };
        }

        public static Reservation ToReservation(this AddReservationDto addReservationDto)
        {
            return new Reservation()
            {
                StartDate = addReservationDto.StartDate,
                EndDate = addReservationDto.EndDate,
                RoomId = addReservationDto.RoomId
            };
        }

        public static Reservation ToReservation(this UpdateReservationDto updateReservationDto, Reservation reservationDb)
        {
            reservationDb.StartDate = updateReservationDto.StartDate;
            reservationDb.EndDate = updateReservationDto.EndDate;
            reservationDb.RoomId = updateReservationDto.RoomId;

            return reservationDb;
        }
    }
}
