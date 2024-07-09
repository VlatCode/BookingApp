using HostelBookingSystem.DTOs.Hostel;
using HostelBookingSystem.Models;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HostelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private IHostelService _hostelService;

        public HostelController(IHostelService hostelService)
        {
            _hostelService = hostelService;
        }

        [HttpGet]
        public ActionResult<List<HostelDto>> GetAll()
        {
            try
            {
                return Ok(_hostelService.GetAllHostels());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<HostelDto> GetById(int id)
        {
            try
            {
                var hostelDto = _hostelService.GetById(id);
                return Ok(hostelDto);
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

        [HttpPost("addHostel")]
        public IActionResult AddHostel([FromBody] AddHostelDto addHostelDto)
        {
            try
            {
                _hostelService.AddHostel(addHostelDto);
                return StatusCode(StatusCodes.Status201Created);
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

        [HttpDelete("deleteHostel/{id}")]
        public IActionResult DeleteHostel(int id)
        {
            try
            {
                _hostelService.DeleteHostel(id);
                return Ok($"Hostel with id {id} successfully deleted.");
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
