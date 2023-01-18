using HostelBookingSystem.DataAccess;
using HostelBookingSystem.DataAccess.Implementations;
using HostelBookingSystem.DTOs;
using HostelBookingSystem.Mappers;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private IRepository<Room> _roomRepository;

        public RoomService(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public List<RoomDto> GetAllRooms()
        {
            var roomsDb = _roomRepository.GetAll();
            // .ToRoomDto() comes from the mapper
            return roomsDb.Select(x => x.ToRoomDto()).ToList();
        }

        public RoomDto GetById(int id)
        {
            Room roomDb = _roomRepository.GetById(id);
            if (roomDb == null)
            {
                throw new NotFoundException($"Room with id {id} was not found!");
            }
            RoomDto roomDto = roomDb.ToRoomDto();
            return roomDto;
        }

        public void AddRoom(AddRoomDto addRoomDto)
        {
            // 1. Validate the data that we receive
            if (addRoomDto.Hostel == null)
            {
                throw new InvalidEntryException("Invalid entry. Try again.");
            }
            // 2. Map to domain model
            Room newRoom = addRoomDto.ToRoom();
            // 3. Add to db
            _roomRepository.Add(newRoom);
        }
    }
}
