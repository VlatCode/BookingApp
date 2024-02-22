using HostelBookingSystem.DTOs.Room;
using HostelBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Mappers
{
    public static class RoomMapper
    {
        public static RoomDto ToRoomDto(this Room room)
        {
            return new RoomDto
            {
                Id = room.Id,
                HostelName = room.Hostel.Name
            };
        }

        public static Room ToRoom(this AddRoomDto addRoomDto)
        {
            return new Room()
            {
                HostelId = addRoomDto.HostelId, // FK
            };
        }
    }
}
