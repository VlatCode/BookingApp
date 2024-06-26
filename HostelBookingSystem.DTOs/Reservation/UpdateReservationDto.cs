﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DTOs.Reservation
{
    public class UpdateReservationDto
    {
        public int Id { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
