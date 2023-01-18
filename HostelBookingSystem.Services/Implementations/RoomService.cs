﻿using HostelBookingSystem.DataAccess;
using HostelBookingSystem.DataAccess.Implementations;
using HostelBookingSystem.DTOs;
using HostelBookingSystem.Mappers;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelBookingSystem.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Hostel> _hostelRepository;

        // At first, we need to make an instance of the repository
        // because it is needed as a parameter to instantiate the service
        public RoomService(IRepository<Room> roomRepository, IRepository<Hostel> hostelRepository)
        {
            _roomRepository = roomRepository;
            _hostelRepository = hostelRepository;
        }

        public List<RoomDto> GetAllRooms()
        {
            var roomsDb = _roomRepository.GetAll();
            // .ToRoomDto() comes from the RoomMapper
            // We can use LINQ on it because it's an extension method in the RoomMapper
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

        public void AddRoom(AddRoomDto room)
        {
            // 1. Validate the data that we receive
            Hostel hostelDb = _hostelRepository.GetById(room.HostelId);
            if (hostelDb == null)
            {
                throw new NotFoundException($"Hostel with id {room.HostelId} does not exist!");
            }
            // 2. Map to domain model
            Room newRoom = room.ToRoom();
            newRoom.Hostel = hostelDb;
            // 3. Add to db
            _roomRepository.Add(newRoom);
        }
    }
}
