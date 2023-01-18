using HostelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DTOs
{
    public class ReservationDto
    {
        public int StartDate { get; set; }
        public int EndDate { get; set; }
        public int RoomNumber { get; set; }
    }
}
