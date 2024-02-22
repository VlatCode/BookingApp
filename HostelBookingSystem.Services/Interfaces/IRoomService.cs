using HostelBookingSystem.DTOs.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Services.Interfaces
{
    public interface IRoomService
    {
        List<RoomDto> GetAllRooms();
        RoomDto GetById(int id);
        void AddRoom(AddRoomDto room);
        void DeleteRoom(int id);
    }
}
