using HostelBookingSystem.DTOs;
using HostelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Mappers
{
    public static class HostelMapper
    {
        public static HostelDto ToHostelDto(this Hostel hostel)
        {
            return new HostelDto
            {
                Name = hostel.Name,
                NumberOfRooms = hostel.Rooms.Count
            };
        }

        public static Hostel ToHostel(this AddHostelDto addHostelDto)
        {
            return new Hostel()
            {
                Name = addHostelDto.Name,
            };
        }
    }
}
