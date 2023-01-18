using HostelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public List<Reservation> Reservations = new List<Reservation>();
        public Hostel Hostel { get; set; }
    }
}
