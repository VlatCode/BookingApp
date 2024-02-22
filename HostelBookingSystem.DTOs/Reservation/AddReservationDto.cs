using HostelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DTOs.Reservation
{
    public class AddReservationDto
    {
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int RoomId { get; set; }
    }
}
