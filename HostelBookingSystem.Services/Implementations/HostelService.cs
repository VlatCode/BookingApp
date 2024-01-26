using HostelBookingSystem.DataAccess;
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
    public class HostelService : IHostelService
    {
        private IRepository<Hostel> _hostelRepository;

        // At first, we need to make an instance of the repository
        // because it needs to be a given parameter for the service
        // because the service is needed for instantiating the controller
        public HostelService(IRepository<Hostel> hostelRepository)
        {
            _hostelRepository = hostelRepository;
        }

        public List<HostelDto> GetAllHostels()
        {
            var hostelsDb = _hostelRepository.GetAll();
            // .ToHostelDto() comes from the mapper
            return hostelsDb.Select(x => x.ToHostelDto()).ToList();
        }

        public HostelDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEntryException("Hostel ID must be a positive integer.");
            }

            Hostel hostelDb = _hostelRepository.GetById(id);
            if (hostelDb == null)
            {
                throw new NotFoundException($"Hostel with id {id} was not found!");
            }
            if (id == null)
            {
                throw new InvalidEntryException("Hostel ID is required");
            }

            HostelDto hostelDto = hostelDb.ToHostelDto();
            hostelDto.NumberOfRooms = hostelDb.Rooms.Count;
            return hostelDto;
        }

        public void AddHostel(AddHostelDto addHostelDto)
        {
            // 1. Validate the data that we receive
            if (string.IsNullOrEmpty(addHostelDto.Name))
            {
                throw new NotFoundException("Name field is required!");
            }
            if (addHostelDto.Name.Length > 100)
            {
                throw new InvalidEntryException("Invalid entry. Try again.");
            }
            if (_hostelRepository.GetAll().Any(h => h.Name == addHostelDto.Name))
            {
                throw new InvalidEntryException("Hostel with the same name already exists.");
            }
            if (addHostelDto.NumberOfRooms <= 0)
            {
                throw new InvalidEntryException("Number of rooms must be a positive integer.");
            }
            // 2. Map to domain model
            Hostel newHostel = addHostelDto.ToHostel();
            List<Room> rooms = new List<Room>();

            for (int i = 0; i < addHostelDto.NumberOfRooms; i++)
            {
                rooms.Add(new Room());
            }

            newHostel.Rooms = rooms;
            // 3. Add to db
            _hostelRepository.Add(newHostel);
        }

        public void DeleteHostel(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEntryException("Hostel ID must be a positive integer.");
            }

            Hostel hostelDb = _hostelRepository.GetById(id);
            if (hostelDb == null)
            {
                throw new NotFoundException($"Hostel with id {id} was not found!");
            }
            _hostelRepository.Delete(hostelDb);
        }
    }
}
