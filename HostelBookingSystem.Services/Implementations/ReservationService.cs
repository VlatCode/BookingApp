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
using HostelBookingSystem.Shared.CustomExceptions;

namespace HostelBookingSystem.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private IRepository<Reservation> _reservationRepository;
        private IRepository<Room> _roomRepository;



        // At first, we need to make an instance of the repository
        // because it needs to be a given parameter for the service
        public ReservationService(IRepository<Reservation> reservationRepository, IRepository<Room> roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }
        public List<ReservationDto> GetAllReservations()
        {
            var reservationsDb = _reservationRepository.GetAll();
            return reservationsDb.Select(x => x.ToReservationDto()).ToList();
        }

        public ReservationDto GetById(int id)
        {
            Reservation reservationDb = _reservationRepository.GetById(id);
            if (reservationDb == null)
            {
                throw new NotFoundException($"Reservation with id {id} was not found.");
            }

            ReservationDto reservationDto = reservationDb.ToReservationDto();
            return reservationDto;
        }

        public void AddReservation(AddReservationDto reservation)
        {
            // 1. Validation
            Room roomDb = _roomRepository.GetById(reservation.RoomId);
            if (roomDb == null)
            {
                throw new NotFoundException($"Room with id {reservation.RoomId} was not found!");
            }
            if (reservation.StartDate == null || reservation.EndDate == null)
            {
                throw new InvalidEntryException($"Please select booking dates!");
            }
            // 2. Map to domain model
            Reservation newReservation = reservation.ToReservation();
            newReservation.Room = roomDb;
            // 3. Add to db
            _reservationRepository.Add(newReservation);
        }

        public void UpdateReservation(UpdateReservationDto reservation)
        {
            // 1. Validation
            Reservation reservationDb = _reservationRepository.GetById(reservation.Id);
            if (reservationDb == null)
            {
                throw new NotFoundException($"Reservation with id {reservation.Id} was not found!");
            }

            Room roomDb = _roomRepository.GetById(reservation.RoomId);
            if (roomDb == null)
            {
                throw new InvalidEntryException($"Room id {reservation.RoomId} does not exist! Try again.");
            }

            if (reservation.StartDate == null || reservation.EndDate == null)
            {
                throw new NotFoundException($"Please enter start and end dates!");
            }
            // 2. Update
            reservationDb.StartDate = reservation.StartDate;
            reservationDb.EndDate = reservation.EndDate;
            reservationDb.RoomId = reservation.RoomId;

            _reservationRepository.Update(reservationDb);
        }

        public void DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }
    }
}
