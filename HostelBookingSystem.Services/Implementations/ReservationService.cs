using HostelBookingSystem.DataAccess.Implementations;
using HostelBookingSystem.DataAccess;
using HostelBookingSystem.DTOs;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelBookingSystem.Mappers;

namespace HostelBookingSystem.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private IRepository<Reservation> _reservationRepository;

        // At first, we need to make an instance of the repository
        // because it needs to be a given parameter for the service
        public ReservationService(IRepository<Reservation> reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public void AddReservation(AddReservationDto room)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public List<ReservationDto> GetAllReservations()
        {
            var reservationsDb = _reservationRepository.GetAll();
            return reservationsDb.Select(x => x.ToReservationDto()).ToList();
        }

        public ReservationDto GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
