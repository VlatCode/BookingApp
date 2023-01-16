using HostelBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Hostel>> GetAll()
        {
            return Ok(StaticDb.Hostels);
        }

        [HttpGet("index")]
        public ActionResult<Hostel> GetByIndex(int index)
        {
            try
            {
                if (index < 0)
                {
                    return BadRequest("The index can not be negative");
                }
                if (index >= StaticDb.Hostels.Count)
                {
                    return NotFound($"There is no resource on index {index}");
                }

                return Ok(StaticDb.Hostels[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] Hostel hostel)
        {
            try
            {
                if (string.IsNullOrEmpty(hostel.Name))
                {
                    return BadRequest("Please add hostel name!");
                }

                StaticDb.Hostels.Add(hostel);
                return StatusCode(StatusCodes.Status201Created, "Hostel added.");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }
    }
}
