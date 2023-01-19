using HostelBookingSystem.DTOs;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Implementations;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
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

        [HttpGet("{id}")]
        public ActionResult<ReservationDto> GetById(int id)
        {
            try
            {
                var reservationDto = _reservationService.GetById(id);
                return Ok(reservationDto);
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost("addReservation")]
        public IActionResult Add([FromBody] AddReservationDto addReservationDto)
        {
            try
            {
                _reservationService.AddReservation(addReservationDto);
                return StatusCode(StatusCodes.Status201Created, "Reservation created.");
            }
            catch (InvalidEntryException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPut]
        public IActionResult UpdateReservation([FromBody] UpdateReservationDto updateReservationDto)
        {
            try
            {
                _reservationService.UpdateReservation(updateReservationDto);
                return NoContent(); // 204
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message); // 404
            }
            catch (InvalidEntryException e)
            {
                return BadRequest(e.Message); // 400
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }


        [HttpDelete("deleteReservation/{id}")]
        public IActionResult DeleteReservation(int id)
        {
            try
            {
                _reservationService.DeleteReservation(id);
                return Ok($"Reservation with id {id} successfully deleted.");
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }
    }
}
