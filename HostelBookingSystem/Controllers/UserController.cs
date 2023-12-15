using HostelBookingSystem.Domain.Models;
using HostelBookingSystem.DTOs;
using HostelBookingSystem.Services.Implementations;
using HostelBookingSystem.Services.Interfaces;
using HostelBookingSystem.Shared.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace HostelBookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IConfiguration _configuration; // This is used to create a Jwt token in CreateToken()

        // The service is a parameter for the controller
        // because it's required for the controller to be instantiated
        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
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

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(AddUserDto user)
        {
            try
            {
                _userService.Register(user);
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

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto loginInfo)
        {
            var allUsers = _userService.GetAllUsers();

            if (loginInfo.Username == null || loginInfo.Password == null)
            {
                return BadRequest("Username and password are required!");
            }

            var matchedUser = allUsers.FirstOrDefault(x => x.Username == loginInfo.Username);

            if (matchedUser == null)
            {
                return BadRequest("User not found.");
            }

            // Hash the input password and compare with the stored hashed password
            byte[] providedPasswordHash;
            byte[] providedPasswordSalt;
            CreatePasswordHash(loginInfo.Password, out providedPasswordHash, out providedPasswordSalt);

            if (!VerifyPasswordHash(loginInfo.Password, matchedUser.PasswordHash, matchedUser.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = CreateToken(matchedUser);

            return Ok(token);
        }

        private string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username)
    };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            // Return the token as a JSON object
            return $"{{\"token\":\"{jwt}\"}}";
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
