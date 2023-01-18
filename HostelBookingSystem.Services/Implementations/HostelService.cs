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
            Hostel hostelDb = _hostelRepository.GetById(id);
            if(hostelDb == null)
            {
                throw new NotFoundException($"Hostel with id {id} was not found!");
            }
            HostelDto hostelDto = hostelDb.ToHostelDto();
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
            // 2. Map to domain model
            Hostel newHostel = addHostelDto.ToHostel();
            // 3. Add to db
            _hostelRepository.Add(newHostel);
        }
    }
}
