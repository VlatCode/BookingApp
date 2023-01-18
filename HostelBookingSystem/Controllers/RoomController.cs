using HostelBookingSystem.DTOs;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Implementations;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // GET ALL ROOMS
        [HttpGet]
        public ActionResult<List<RoomDto>> Get()
        {
            try
            {
                return Ok(_roomService.GetAllRooms());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        // GET ROOM BY ID
        [HttpGet("{id}")]
        public ActionResult<RoomDto> GetById(int id)
        {
            try
            {
                var roomDto = _roomService.GetById(id);
                return Ok(roomDto);
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

        // ADD ROOM
        [HttpPost("addRoom")]
        public IActionResult AddRoom([FromBody] AddRoomDto addRoomDto) 
        { 
            try
            {
                _roomService.AddRoom(addRoomDto);
                return StatusCode(StatusCodes.Status201Created, "Room added.");
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
    }
}
