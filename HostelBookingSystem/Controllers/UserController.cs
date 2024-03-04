using HostelBookingSystem.DTOs.User;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Services.Implementations;
using HostelBookingSystem.Shared.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HostelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> GetAll()
        {
            try
            {
                return Ok(_userService.GetAllUsers());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred! Contact the admin!");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult<LoggedUserDataDto> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                var userDto = _userService.RegisterUser(registerUserDto);

                return StatusCode(StatusCodes.Status201Created, userDto);
            }
            catch (UserDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [AllowAnonymous] //no token needed (we can not be logged in before login)
        [HttpPost("login")]
        public ActionResult<LoggedUserDataDto> Login([FromBody] LoginUserDto loginDto)
        {
            try
            {
                var userDto = _userService.LoginUser(loginDto);
                return Ok(userDto);
            }
            catch (UserDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Logout successful.");
        }
    }
}
