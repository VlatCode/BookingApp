using HostelBookingSystem.DTOs;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Implementations;
using HostelBookingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace HostelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        // -After checking availability, the admin should be able to make a reservation,
        // cancel a reservation
        // or update a reservation.

        private IReservationService _reservationService;

        // The service is a parameter for the controller
        // because it's required for the controller to be instantiated
        public ReservationController(IReservationService reservationService) // Dependency Injection
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public ActionResult<List<ReservationDto>> Get()
        {
            try
            {
                return Ok(_reservationService.GetAllReservations());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("index")]
        public ActionResult<Reservation> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative");
                }
                if (index >= StaticDb.Reservations.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Reservations[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Reservation reservation)
        {
            try
            {
                if (reservation.Id == null)
                {
                    return BadRequest("Please enter reservation number.");
                }
                // VALIDATE ROOM AVAILABILITY
                //if (room.Availability != true)
                //{
                //    return BadRequest("This room is not available.");
                //}

                StaticDb.Reservations.Add(reservation);
                return StatusCode(StatusCodes.Status201Created, "Reservation was successful.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpDelete("id")]
        public ActionResult<List<Reservation>> Delete(int id)
        {
            var reservation = StaticDb.Reservations.Find(x => x.Id == id);
            if (reservation == null)
            {
                return BadRequest("Reservation not found.");
            }
            StaticDb.Reservations.Remove(reservation);
            return Ok(reservation);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Reservation reservation, int id)
        {
            Reservation reservationDb = StaticDb.Reservations.Find(x => x.Id == id);
            if (reservationDb == null)
            {
                return BadRequest("Reservation not found.");
            }
            //if (reservationDb.MainGuestId == null)
            //{
            //    return BadRequest("Reservation not found.");
            //}
            //if (reservationDb.StartDate == null || reservationDb.EndDate == null)
            //{
            //    return BadRequest("Reservation not found.");
            //}

            // Update
            reservationDb.Id = reservation.Id;
            reservationDb.StartDate = reservation.StartDate;
            reservationDb.EndDate = reservation.EndDate;

            return Ok(reservationDb);
        }
    }
}
