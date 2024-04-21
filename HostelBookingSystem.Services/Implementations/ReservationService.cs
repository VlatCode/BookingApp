using HostelBookingSystem.DataAccess.Implementations;
using HostelBookingSystem.DataAccess;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HostelBookingSystem.Mappers;
using HostelBookingSystem.Shared.CustomExceptions;
using HostelBookingSystem.DTOs.Reservation;
using HostelBookingSystem.Domain.Models;

namespace HostelBookingSystem.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private IRepository<Reservation> _reservationRepository;
        private IRepository<Room> _roomRepository;
        private DataAccess.Interfaces.IRepository<User> _userRepository;



        // At first, we need to make an instance of the repository
        // because it needs to be a given parameter for the service
        public ReservationService(
            IRepository<Reservation> reservationRepository, 
            IRepository<Room> roomRepository,
            DataAccess.Interfaces.IRepository<User> userRepository
            )
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _userRepository = userRepository;
        }
        public List<ReservationDto> GetAllReservations()
        {
            var reservationsDb = _reservationRepository.GetAll();
            return reservationsDb.Select(x => x.ToReservationDto()).ToList();
        }

        public ReservationDto GetById(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEntryException("Reservation ID must be a positive integer.");
            }

            Reservation reservationDb = _reservationRepository.GetById(id);
            if (reservationDb == null)
            {
                throw new NotFoundException($"Reservation with id {id} was not found.");
            }

            ReservationDto reservationDto = reservationDb.ToReservationDto();
            //reservationDto.NumberOfGuests = reservationDto.Guests.Count;
            return reservationDto;
        }

        public void AddReservation(AddReservationDto reservation)
        {
            // 1. Validation
            if (reservation.RoomId <= 0)
            {
                throw new InvalidEntryException("Room ID must be a positive integer.");
            }
            Room roomDb = _roomRepository.GetById(reservation.RoomId);
            if (roomDb == null)
            {
                throw new NotFoundException($"Room with id {reservation.RoomId} was not found!");
            }
            if (reservation.StartDate <= 0 || reservation.EndDate <= 0)
            {
                throw new InvalidEntryException($"Please select booking dates!");
            }
            User userDb = _userRepository.GetById(reservation.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"User with id {reservation.UserId} was not found!");
            }
            // 2. Map to domain model
            Reservation newReservation = reservation.ToReservation();
            newReservation.Room = roomDb;
            newReservation.User = userDb;
            if (!string.IsNullOrEmpty(reservation.UserName))
            {
                newReservation.UserName = reservation.UserName;
            }
            newReservation.NumberOfGuests = reservation.NumberOfGuests;
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
            if (reservation.RoomId <= 0)
            {
                throw new InvalidEntryException("Hostel ID must be a positive integer.");
            }

            Room roomDb = _roomRepository.GetById(reservation.RoomId);
            if (roomDb == null)
            {
                throw new InvalidEntryException($"Room id {reservation.RoomId} does not exist! Try again.");
            }

            if (reservation.StartDate <= 0 || reservation.EndDate <= 0)
            {
                throw new NotFoundException($"Please enter start and end dates!");
            }
            User userDb = _userRepository.GetById(reservation.UserId);
            if (userDb == null)
            {
                throw new NotFoundException($"User with id {reservation.UserId} was not found!");
            }
            // 2. Update
            reservationDb.StartDate = reservation.StartDate;
            reservationDb.EndDate = reservation.EndDate;
            reservationDb.RoomId = reservation.RoomId;
            reservationDb.UserId = reservation.UserId;
            if (!string.IsNullOrEmpty(reservation.UserName))
            {
                reservationDb.UserName = reservation.UserName;
            }
            reservationDb.NumberOfGuests = reservation.NumberOfGuests;
            _reservationRepository.Update(reservationDb);
        }

        public void DeleteReservation(int id)
        {
            if (id <= 0)
            {
                throw new InvalidEntryException("Reservation ID must be a positive integer.");
            }

            Reservation reservationDb = _reservationRepository.GetById(id);
            if (reservationDb == null)
            {
                throw new NotFoundException($"Reservation with id {id} was not found.");
            }

            _reservationRepository.Delete(reservationDb);
        }
    }
}
