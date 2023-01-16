using HostelBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        [HttpGet("index")]
        public ActionResult<Room> GetByIndex (int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative");
                }
                if (index >= StaticDb.Rooms.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Rooms[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Room room)
        {
            try
            {
                if (room.Id == null)
                {
                    return BadRequest("Room number must not be empty!");
                }
                if (room.Availability != true)
                {
                    return BadRequest("This room is not available.");
                }

                StaticDb.Rooms.Add(room);
                return StatusCode(StatusCodes.Status201Created, "Room added.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }
    }
}
